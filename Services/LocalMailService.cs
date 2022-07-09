namespace DotNetCoreWebAPI.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailFrom = string.Empty;
        private readonly string _mailTo = string.Empty;


        public LocalMailService(IConfiguration configuration)
        {
            _mailFrom = Convert.ToString(configuration["MailAddress:MailFrom"]);
            _mailTo = Convert.ToString(configuration["MailAddress:MailTo"]);
        }

        public void SendMail(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}