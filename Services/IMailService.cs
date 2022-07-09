namespace DotNetCoreWebAPI.Services
{
    public interface IMailService
    {
        public void SendMail(string subject, string message);
    }
}