using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public ProductSpecification(ProductSpecParams spec)
        {
            ApplyIncludes();
            ApplyOrderBy(spec.OrderBy);
            ApplyCreterias(spec);
            ApplyPaging(spec.PageSize, spec.PageIndex);
            OrderDirection = spec.OrderDirection;
        }

        private void ApplyCreterias(ProductSpecParams spec)
        {
            if (spec.ProductBrandId.HasValue)
            {
                base.AddCreteria(p => p.ProductBrandId == spec.ProductBrandId);
            }

            if (spec.ProductTypeId.HasValue)
            {
                base.AddCreteria(p => p.ProductTypeId == spec.ProductTypeId);
            }

            if (!string.IsNullOrEmpty(spec.Search))
            {
                base.AddCreteria(p => p.Name.ToLower().Contains(spec.Search));
            }
        }

        private void ApplyOrderBy(string fieldName)
        {
            var fieldsToSort = new Dictionary<string, Expression<Func<Product, object>>>
            {
                { "id", p => p.Id }, { "name", p => p.Name}, { "price", p => p.Price}
            };

            if (!string.IsNullOrEmpty(fieldName))
            {
                base.AddOrderBy(fieldsToSort[fieldName]);
            }
            else
            {
                base.AddOrderBy(p => p.Id);
            }
        }

        private void ApplyIncludes()
        {
            base.AddInclude(p => p.ProductBrand);
            base.AddInclude(p => p.ProductType);
        }
    }
}
