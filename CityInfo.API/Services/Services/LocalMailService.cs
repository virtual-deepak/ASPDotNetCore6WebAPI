namespace Services.Services;

public class LocalMailService : IMailService
{
    public void Send(string subject, string message)
    {
        Console.WriteLine($"Sending email from {nameof(LocalMailService)} with subject: {subject} and message: {message}");
    }
}