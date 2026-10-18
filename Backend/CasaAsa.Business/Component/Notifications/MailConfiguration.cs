namespace CasaAsa.Business.Component.Notifications
{
    public class MailConfiguration
    {
        public const string SECTION_NAME = "SMTP";

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
