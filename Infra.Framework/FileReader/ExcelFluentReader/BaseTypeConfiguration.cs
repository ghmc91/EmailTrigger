using System.Linq.Expressions;

namespace Infra.Framework.FileReader.ExcelFluentReader
{
    public abstract class BaseTypeConfiguration<T>
    {
        protected IList<PropertyConfiguration> Mappings { get; set; }

        public BaseTypeConfiguration()
        {
            Mappings= new List<PropertyConfiguration>();    
        }

        public PropertyConfiguration Map<TProperty>(Expression<Func<T,TProperty>> expression)
        {
            var memberExpression = GetMemberExpression(expression);
            var map = new PropertyConfiguration
            {
                Member = memberExpression.Member
            };

            Mappings.Add(map);
            return map;
        }

        public IList<PropertyConfiguration> GetMappings()
        {
            return Mappings;
        }

        protected internal MemberExpression GetMemberExpression<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (!(expression.Body is MemberExpression member))
            {
                throw new ArgumentException("Expressão inválida");
            }
            return member;
        }
    }
}
