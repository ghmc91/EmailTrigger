using EmailTrigger.Domain.Entities;

namespace EmailTrigger.Domain.Interfaces.Reposiories
{
    public interface ISellerRepository
    {
        public IEnumerable<Seller> GetSellers();    
    }
}
