using System.Net;
using System.Net.Mail;
using EmailWorker.Interfaces;

namespace EmailWorker.Services;

public class EmailSender : IEmailSender, IDisposable
{
    private readonly SmtpClient _smtpClient;
    private readonly MailAddress _sender;
    
    public EmailSender()
    {
        _smtpClient = new SmtpClient();
        _smtpClient.Host = "smtp.gmail.com";
        _smtpClient.Port = 587;
        _smtpClient.EnableSsl = true;
        
        NetworkCredential credentials = new NetworkCredential();
        credentials.UserName="raiffeisen.clone@gmail.com";
        credentials.Password="mdsktqxieyhyqwpj";
        _smtpClient.Credentials = credentials;
        
        //create sender address
        _sender = new MailAddress("raiffeisen.clone@gmail.com", "raiffeisen.clone@gmail.com");
    }
    
    public void Send(string email, string subject, string body)
    {
        MailAddress receiver = new MailAddress(email, email);
        MailMessage message = new MailMessage(_sender, receiver);
        message.Subject = subject;
        message.Body = body;
        _smtpClient.Send(message);
    }

    public void Dispose()
    {
        _smtpClient.Dispose();
    }
}