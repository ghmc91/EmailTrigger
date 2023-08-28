using Infra.Framework.Interfaces;
using Microsoft.Exchange.WebServices.Data;
using System.Net;
using MicrosoftExchange = Microsoft.Exchange.WebServices.Data;

namespace Infra.Framework.Email
{
    public sealed class ExchangeService : IExchangeService
    {
        private const int MAX_EMAILS_RETURN = 1000;
        private readonly ICredentials _credentials;
        private readonly MicrosoftExchange.ExchangeService _service = new MicrosoftExchange.ExchangeService(MicrosoftExchange.ExchangeVersion.Exchange2013);
        private List<string> _loadAttachements;

        public ExchangeService()
        {

        }

        public ExchangeService(ICredentials credentials)
        {
            _loadAttachements= new List<string>();
            _credentials = credentials;
            _service.TraceEnabled = true;
        }
        
        public string SendMail(string from, string subject, string body, string[] to, string[] pathAttachment = null, string[] cc = null)
        {
            var message = CreateMessage(subject, body, to, cc, from);
            if (pathAttachment != null && pathAttachment.Any())
            {
                foreach (var path in pathAttachment)
                {
                    message.Attachments.AddFileAttachment(path);
                }
            }
            return SendMail(from, message);
        }

        string SendMail(string from, EmailMessage message)
        {
            try
            {
                
                
                message.Save(new FolderId(WellKnownFolderName.Drafts, new Mailbox(from)));
                message.SendAndSaveCopy(new FolderId(WellKnownFolderName.SentItems, new Mailbox(from)));
                return message.Id.UniqueId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        EmailMessage CreateMessage(string subject, string body, string[] to, string[] cc, string from)
        {
            var message = new EmailMessage(_service);
            message.Subject = subject;  
            message.Body = body;
            message.Body.BodyType = BodyType.HTML;
            if (!string.IsNullOrEmpty(from))
                message.From = from;
            foreach (var addressTo in to)
            {
                if (!string.IsNullOrEmpty(addressTo))
                    message.ToRecipients.Add(addressTo.Trim());
            }
            if (cc != null)
            {
                foreach (var addressCc in cc)
                {
                    if (!string.IsNullOrEmpty(addressCc))
                        message.CcRecipients.Add(addressCc.Trim());
                }
            }
            return message;
        }

        public void LoadMailBox(ExchangeCredential credential)
        {
            try
            {

                _loadAttachements = new List<string>();
                _service.Url = new Uri(credential.Server);
                if (credential.UseDefaultCredentials)
                {
                    _service.UseDefaultCredentials = true;
                }
                else
                {
                    _service.Credentials = credential.GetNetworkCredential();
                }
                ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => true;

                _service.Timeout = 5 * 60 * 1000;
                _service.KeepAlive = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
