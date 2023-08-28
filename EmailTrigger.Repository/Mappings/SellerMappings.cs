using EmailTrigger.Domain.Entities;
using Infra.Framework.FileReader.ExcelFluentReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTrigger.Infra.Data.Mappings
{
    public class SellerMappings : BaseTypeConfiguration<Seller>
    {
        public SellerMappings()
        {
            Map(x => x.Store)
               .WithColumnName("Loja");

            Map(x => x.Email)
               .WithColumnName("Email");

            Map(x => x.Code)
               .WithColumnName("Ddd");
            
            Map(x => x.Name)
               .WithColumnName("Responsavel");

            Map(x => x.Fone)
               .WithColumnName("Telefone");

        }
    }
}
