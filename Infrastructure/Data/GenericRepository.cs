using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllItemsAsync()
        {
            DbSet<TEntity> entity = _context.Set<TEntity>();
            return await entity.ToArrayAsync<TEntity>();
        }

        public async Task<TEntity> GetItemByIdAsync(int id)
        {
            DbSet<TEntity> entity = _context.Set<TEntity>();
            return await entity.FindAsync(id);
        }

        public async Task<TEntity> GetEntityWithSpecAsync(ISpecification<TEntity> specification)
        {
            IQueryable<TEntity> queryEvaluation = ApplySpecification(specification);
            return await queryEvaluation.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesWithSpecAsync(ISpecification<TEntity> specification)
        {
            IQueryable<TEntity> queryEvaluation = ApplySpecification(specification);
            return await queryEvaluation.ToArrayAsync();
        }

        public async Task<int> GetTotalCountAsync(ISpecification<TEntity> specification)
        {
            IQueryable<TEntity> queryEvaluation = ApplySpecification(specification);
            return await queryEvaluation.CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.Evaluate(specification, _context);
        }
    }
}
