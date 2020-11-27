using Core.Entities;

namespace Core.Specifications
{
    public class ProductPagingSpecification : BaseSpecifiction<Product>
    {
        public ProductPagingSpecification(ProductSpecParams specParams)
        {
            if (specParams.ProductTypeId.HasValue)
            {
                base.AddCreteria(p => p.ProductTypeId == specParams.ProductTypeId);
            }

            if (specParams.ProductBrandId.HasValue)
            {
                base.AddCreteria(p => p.ProductBrandId == specParams.ProductBrandId);
            }

            if (!string.IsNullOrEmpty(specParams.Search))
            {
                base.AddCreteria(p => p.Name.ToLower().Contains(specParams.Search));
            }
        }
    }
}
