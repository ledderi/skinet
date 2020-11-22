using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> Evaluate(ISpecification<TEntity> specification, DbContext context)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (specification.Creteria != null)
            {
                query = query.Where(specification.Creteria);
            }

            if (specification.Includes.Any())
            {
                //foreach (var include in specification.Includes)
                //{
                //    query = query.Include(include);
                //}

                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
