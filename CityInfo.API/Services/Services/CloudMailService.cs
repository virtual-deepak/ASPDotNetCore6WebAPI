namespace Services.Services;

public class CloudMailService : IMailService
{
    public void Send(string subject, string message)
    {
        Console.WriteLine($"Sending email from {nameof(CloudMailService)} with subject: {subject} and message: {message}");
    }
}
