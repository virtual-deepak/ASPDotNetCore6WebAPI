namespace Services.Services;

public interface IMailService
{
    public void Send(string subject, string message);
}
