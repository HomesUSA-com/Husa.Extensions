namespace Husa.Extensions.EmailNotification.Classes
{
    public class Recipient
    {
        public Recipient(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
