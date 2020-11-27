using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public abstract class BaseSpecifiction<T> : ISpecification<T> where T : BaseEntity
    {
        private int? _pageSize;
        private int? _pageIndex;
        private Expression<Func<T, object>> _orderBy = null;
        private List<Expression<Func<T, bool>>> _creterias = new List<Expression<Func<T, bool>>>();
        private List<Expression<Func<T, object>>> _includes = new List<Expression<Func<T, object>>>();

        public BaseSpecifiction(){}
        public BaseSpecifiction(Expression<Func<T, bool>> creteria)
        {
            _creterias.Add(creteria);
        }

        public List<Expression<Func<T, bool>>> Creterias => _creterias;
        public List<Expression<Func<T, object>>> Includes => _includes;
        public Expression<Func<T, object>> OrderBy => _orderBy;
        public string OrderDirection { get; set; }
        public int? PageSize => _pageSize;
        public int? PageIndex => _pageIndex;


        protected void AddInclude(Expression<Func<T, object>> include)
        {
            _includes.Add(include);
        }

        protected void AddOrderBy(Expression<Func<T, object>> fieldName)
        {
            _orderBy = fieldName;
        }

        protected void AddCreteria(Expression<Func<T, bool>> creteria)
        {
            _creterias.Add(creteria);
        }

        protected void ApplyPaging(int? pageSize, int? pageIndex)
        {
            _pageSize = pageSize;
            _pageIndex = pageIndex;
        }
    }
}
