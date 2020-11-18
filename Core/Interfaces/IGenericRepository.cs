using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetItemByIdAsync(int id);
        Task<IEnumerable<T>> GetAllItemsAsync();
    }
}
