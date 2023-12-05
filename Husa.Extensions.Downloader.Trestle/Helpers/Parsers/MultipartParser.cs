namespace Husa.Extensions.Downloader.Trestle.Helpers.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    public class MultipartParser
    {
        private readonly MemoryStream input;
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
            this.state = State.Begin;
        }

        private enum State
        {
            Begin,
            BoundaryDef,
            Headers,
            Image,
            Done,
        }

        public IEnumerable<MultipartImage> GetImages()
        {
            byte b;
            this.image = new MemoryStream();
            this.imageWriter = new BinaryWriter(this.image);
            this.headers = new Dictionary<string, string>();

            while (true)
            {
                if (this.state == State.Done)
                {
                    break;
                }

                b = (byte)this.input.ReadByte();
                switch (this.state)
                {
                    case State.Begin:
                        this.GetToBoundaryDef(b);
                        this.state = State.BoundaryDef;
                        break;
                    case State.BoundaryDef:
                        this.GetBoundaryDefinition(b);
                        this.state = State.Headers;
                        break;
                    case State.Headers:
                        this.GetHeaders(b);
                        this.state = State.Image;
                        break;
                    case State.Image:
                        if (b == (byte)'-')
                        {
                            if (this.CheckForBoundary(b))
                            {
                                if (this.state != State.Done)
                                {
                                    this.state = State.Headers;
                                }

                                yield return new MultipartImage(this.headers, this.image);
                                this.image.Close();
                                this.imageWriter.Close();
                                this.headers.Clear();
                                this.image = new MemoryStream();
                                this.imageWriter = new BinaryWriter(this.image);
                            }
                        }
                        else
                        {
                            this.imageWriter.Write(b);
                        }

                        break;
                }
            }
        }

        private void GetToBoundaryDef(byte b)
        {
            while (b != (byte)'-')
            {
                b = (byte)this.input.ReadByte();
            }

            this.boundary += (char)b;
        }

        private void GetBoundaryDefinition(byte b)
        {
            while (b != (byte)'\r')
            {
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop
                this.boundary += (char)b;
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop
                b = (byte)this.input.ReadByte();
            }

            Console.WriteLine(this.boundary);

            // Throw away the \n after the \r
            this.input.ReadByte();
        }

        private void GetHeaders(byte b)
        {
            string buf = string.Empty;
            while (true)
            {
                if (b == (byte)'\r')
                {
                    this.input.ReadByte();
                    if (buf.Length == 0)
                    {
                        return;
                    }

                    string[] h = buf.Split(':');
                    this.headers[h[0].Trim()] = h[1].Trim();
                    buf = string.Empty;
                }
                else
                {
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop
                    buf += (char)b;
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop
                }

                b = (byte)this.input.ReadByte();
            }
        }

        private bool CheckForBoundary(byte b)
        {
            var buf = new MemoryStream();
            while (true)
            {
                buf.WriteByte(b);
                if (!this.boundary.StartsWith(Encoding.Default.GetString(buf.ToArray())))
                {
                    buf.WriteTo(this.image);
                    return false;
                }

                if (this.boundary == Encoding.Default.GetString(buf.ToArray()))
                {
                    b = (byte)this.input.ReadByte();
                    if (b == (byte)'\r')
                    {
                        this.input.ReadByte();
                        return true;
                    }

                    if (b == (byte)'-')
                    {
                        b = (byte)this.input.ReadByte();
                        if (b == (byte)'-')
                        {
                            this.state = State.Done;
                            return true;
                        }

                        buf.WriteTo(this.image);
                        this.image.WriteByte((byte)'-');
                        this.image.WriteByte(b);
                        return false;
                    }

                    buf.WriteTo(this.image);
                    this.image.WriteByte(b);
                    return false;
                }

                b = (byte)this.input.ReadByte();
            }
        }
    }
}
