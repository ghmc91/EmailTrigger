using EmailTrigger.Domain.Entities;
using EmailTrigger.Domain.Interfaces.Reposiories;
using EmailTrigger.Infra.Data.Mappings;

namespace EmailTrigger.Infra.Data.Repositories
{
    public class SellerRepository : BaseRepository, ISellerRepository
    {
        public IEnumerable<Seller> GetSellers()
        {
            var result = ReadRecentFile<Seller, SellerMappings>(x => !String.IsNullOrEmpty(x.Store), 
                                                                "C:\\Users\\Usuario\\Documents\\UaiLivre\\arquivos\\Lojas\\Repositorio\\", 1);
            return result.ToList();
        }
    }
}
