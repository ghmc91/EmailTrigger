using EmailTrigger.Domain.Entities;

namespace EmailTrigger.Domain.Interfaces.Services
{
    public interface ISellerService
    {
        IEnumerable<Seller> GetData();
        void SendSellerEmail();

    }
}
