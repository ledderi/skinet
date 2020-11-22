﻿using Core.Entities;
using Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetItemByIdAsync(int id);
        Task<IEnumerable<T>> GetAllItemsAsync();

        Task<T> GetEntityWithSpecAsync(ISpecification<T> specification);
        Task<IEnumerable<T>> GetEntitiesWithSpecAsync(ISpecification<T> specification);
    }
}
