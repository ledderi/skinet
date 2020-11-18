using Core.Entities;
using Core.Interfaces;
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
    }
}
