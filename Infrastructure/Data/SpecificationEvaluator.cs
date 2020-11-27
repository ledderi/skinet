using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> Evaluate(ISpecification<TEntity> specification, DbContext context)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (specification.Creterias.Any())
            {
                query = specification.Creterias.Aggregate(query, (current, creteria) => current.Where(creteria));
            }

            if (specification.Includes.Any())
            {
                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (specification.OrderBy != null)
            {
                if (specification.OrderDirection == "desc")
                {
                    query = query.OrderByDescending(specification.OrderBy);
                }
                else
                {
                    query = query.OrderBy(specification.OrderBy);
                }
            }

            if (specification.PageIndex.HasValue && specification.PageSize.HasValue)
            {
                query = query.Skip((((int)specification.PageIndex - 1) * (int)specification.PageSize))
                    .Take((int)specification.PageSize);
            }

            return query;
        }
    }
}
