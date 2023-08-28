using Infra.Framework.Email;

namespace Infra.Framework.Interfaces
{
    public interface IExchangeService
    {
        string SendMail(string from, string subject, string body, string[] to, string[] pathAttachment = null, string[] cc = null);
        void LoadMailBox(ExchangeCredential credential);
    }
}
