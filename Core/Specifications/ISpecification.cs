using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        List<Expression<Func<T, bool>>> Creterias { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        string OrderDirection { get; set; }
        Nullable<int> PageIndex { get; }
        Nullable<int> PageSize { get; }
    }
}
