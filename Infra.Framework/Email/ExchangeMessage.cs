using Microsoft.Exchange.WebServices.Data;
using System.Text.RegularExpressions;

namespace Infra.Framework.Email
{
    public class ExchangeMessage
    {
        private Regex _pattern;

        public string UniqueId { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string ConversationId { get; set; }

        public DateTime SentDate { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool HasAttachments { get; set; }

        public IEnumerable<AttachmentMessage> Attachments { get; set; }

        public string Filename
        {
            get
            {
                return string.Format("{0}_{1}", ReceivedDate.ToString("yyyyMMddHHmmss"), _pattern.Replace(Subject.Trim(), string.Empty));
            }
        }

        public ExchangeMessage()
        {
            _pattern = new Regex(@"/| |\|-|:");
        }
    }

    public class AttachmentMessage
    {
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public AttachmentMessage(Attachment attachment)
        {
            ContentId = attachment.ContentId;
            ContentType = attachment.ContentType;
            Id = attachment.Id;
            Name = attachment.Name;
        }
    }
}
