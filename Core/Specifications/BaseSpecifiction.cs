using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public abstract class BaseSpecifiction<T> : ISpecification<T> where T : BaseEntity
    {
        private Expression<Func<T, bool>> _creteria = null;
        private List<Expression<Func<T, object>>> _includes = new List<Expression<Func<T, object>>>();

        public BaseSpecifiction(){}
        public BaseSpecifiction(Expression<Func<T, bool>> creteria)
        {
            _creteria = creteria;
        }

        public Expression<Func<T, bool>> Creteria => _creteria;
        public List<Expression<Func<T, object>>> Includes => _includes;

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            _includes.Add(include);
        }
    }
}
