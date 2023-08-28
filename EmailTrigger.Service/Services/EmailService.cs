using EmailTrigger.Domain.Interfaces.Services;
using Infra.Framework.Email;
using Infra.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTrigger.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IExchangeService _exchangeService;

        public EmailService(IExchangeService exchangeService)
        {
            _exchangeService= exchangeService;
        }


        public void SendEmail(string from, string password, string subject, string body, string[] to, string[] pathAttachment = null, string[] cc = null)
        {
            var exchangeCredential = new ExchangeCredential(from, password, "https://outlook.office365.com/ews/exchange.asmx");

            _exchangeService.LoadMailBox(exchangeCredential);
            _exchangeService.SendMail(from, subject, body, to, pathAttachment, cc);
        }
    }
}
