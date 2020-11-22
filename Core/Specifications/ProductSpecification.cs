using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecifiction<Product>
    {
        public ProductSpecification()
        {
            ApplyIncludes();
        }
        public ProductSpecification(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            base.AddInclude(p => p.ProductBrand);
            base.AddInclude(p => p.ProductType);
        }
    }
}
