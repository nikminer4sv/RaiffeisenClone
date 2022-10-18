namespace EmailWorker.Interfaces;

public interface IEmailSender
{
    void Send(string email, string subject, string body);
}