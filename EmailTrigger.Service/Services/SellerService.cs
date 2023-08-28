using EmailTrigger.Domain.Entities;
using EmailTrigger.Domain.Interfaces.Reposiories;
using EmailTrigger.Domain.Interfaces.Services;

namespace EmailTrigger.Service.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IEmailService _emailService;

        public SellerService(ISellerRepository sellerRepository, IEmailService emailService)
        {
            _sellerRepository = sellerRepository;
            _emailService = emailService;
        }

        public IEnumerable<Seller> GetData()
        {
            
            return _sellerRepository.GetSellers();
        }

        public void SendSellerEmail()
        {
            var sellers = GetData();
            var subject = "Teste";
            var body = "Teste";
            var to = new List<string>() { "gustavo_martins1991@hotmail.com" };
            
            _emailService.SendEmail("contato@uailivreart.com", "UaiLibre22!", subject, body, to.ToArray());
        }
    }
}
