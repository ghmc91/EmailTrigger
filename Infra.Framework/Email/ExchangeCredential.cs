using System.Net;
using MicrosoftExchange = Microsoft.Exchange.WebServices.Data;

namespace Infra.Framework.Email
{
    public class ExchangeCredential
    {
        private ICredentials _credentials = null;

        public string Username { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public string Server { get; set; }

        private readonly MicrosoftExchange.ExchangeService _service = new MicrosoftExchange.ExchangeService(MicrosoftExchange.ExchangeVersion.Exchange2013);

        public bool UseDefaultCredentials
        {
            get
            {
                return _credentials == null && string.IsNullOrEmpty(Username);
            }
        }

        public ExchangeCredential(string username, string password, string server)
        {
            Username = username;
            Password = password;
            Server = server;
            Domain = "uailivreart.com";
        }

        public ExchangeCredential(string _server)
        {
            Server = _server;
        }

        public ExchangeCredential(ICredentials credentials, string server)
            : this(server)
        {
            _credentials = credentials;
        }

        public NetworkCredential GetNetworkCredential()
        {
            return _credentials != null
                                ? _credentials as NetworkCredential
                                : new NetworkCredential(Username, Password, Domain);
        }

        public NetworkCredential GetDefaultNetworkCredentials() 
        {
            return CredentialCache.DefaultNetworkCredentials;
        }
    }
}
