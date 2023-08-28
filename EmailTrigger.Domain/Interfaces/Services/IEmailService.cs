namespace EmailTrigger.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string from, string password, string subject, string body, string[] to, string[] pathAttachment = null, string[] cc = null);
    }
}
