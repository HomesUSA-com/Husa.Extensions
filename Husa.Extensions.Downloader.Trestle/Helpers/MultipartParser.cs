namespace Husa.Extensions.Downloader.Trestle.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Husa.Extensions.Downloader.Trestle.Contracts;

    public class MultipartParser
    {
        private enum State
        {
            Begin,
            BoundaryDef,
            Headers,
            Image,
            Done
        }

        private MemoryStream input;
        private Dictionary<string, string> headers;
        private MemoryStream image;
        private BinaryWriter imageWriter;

        private string boundary;
        private State state;

        public MultipartParser(Stream input)
        {
            this.input = new MemoryStream();
            input.CopyTo(this.input);
            this.input.Seek(0, SeekOrigin.Begin);
            state = State.Begin;
        }

        public IEnumerable<MultipartImage> GetImages()
        {
            byte b;
            image = new MemoryStream();
            imageWriter = new BinaryWriter(image);
            headers = new Dictionary<string, string>();

            while (true)
            {
                if (state == State.Done)
                {
                    break;
                }
                b = (byte)input.ReadByte();
                switch (state)
                {
                    case State.Begin:
                        GetToBoundaryDef(b);
                        state = State.BoundaryDef;
                        break;
                    case State.BoundaryDef:
                        GetBoundaryDefinition(b);
                        state = State.Headers;
                        break;
                    case State.Headers:
                        GetHeaders(b);
                        state = State.Image;
                        break;
                    case State.Image:
                        if (b == (byte)'-')
                        {
                            if (CheckForBoundary(b))
                            {
                                if (state != State.Done)
                                {
                                    state = State.Headers;
                                }

                                yield return new MultipartImage(headers, image);
                                image.Close();
                                imageWriter.Close();
                                headers.Clear();
                                image = new MemoryStream();
                                imageWriter = new BinaryWriter(image);
                            }
                        }
                        else
                        {
                            imageWriter.Write(b);
                        }
                        break;
                }
            }
        }

        private void GetToBoundaryDef(byte b)
        {
            while (b != (byte)'-')
            {
                b = (byte)input.ReadByte();
            }

            boundary += (char)b;
        }

        private void GetBoundaryDefinition(byte b)
        {
            while (b != (byte)'\r')
            {
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop
                boundary += (char)b;
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop
                b = (byte)input.ReadByte();
            }

            Console.WriteLine(boundary);

            // Throw away the \n after the \r
            input.ReadByte();
        }

        private void GetHeaders(byte b)
        {
            string buf = "";
            while (true)
            {
                if (b == (byte)'\r')
                {
                    input.ReadByte();
                    if (buf.Length == 0)
                    {
                        return;
                    }

                    string[] h = buf.Split(':');
                    headers[h[0].Trim()] = h[1].Trim();
                    buf = "";
                }
                else
                {
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop
                    buf += (char)b;
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop
                }

                b = (byte)input.ReadByte();
            }
        }

        private bool CheckForBoundary(byte b)
        {
            var buf = new MemoryStream();
            while (true)
            {
                buf.WriteByte(b);
                if (!boundary.StartsWith(Encoding.Default.GetString(buf.ToArray())))
                {
                    buf.WriteTo(image);
                    return false;
                }
                if (boundary == Encoding.Default.GetString(buf.ToArray()))
                {
                    b = (byte)input.ReadByte();
                    if (b == (byte)'\r')
                    {
                        input.ReadByte();
                        return true;
                    }
                    if (b == (byte)'-')
                    {
                        b = (byte)input.ReadByte();
                        if (b == (byte)'-')
                        {
                            state = State.Done;
                            return true;
                        }
                        buf.WriteTo(image);
                        image.WriteByte((byte)'-');
                        image.WriteByte(b);
                        return false;
                    }
                    buf.WriteTo(image);
                    image.WriteByte(b);
                    return false;
                }


                b = (byte)input.ReadByte();
            }
        }
    }
}
