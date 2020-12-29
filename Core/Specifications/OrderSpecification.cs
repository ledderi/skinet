using Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrderSpecification : BaseSpecifiction<Order>
    {
        public OrderSpecification(string buyerEmail) : base(p => p.BuyerEmail == buyerEmail)
        {
            this.AddIncludes();
            base.AddOrderBy(p => p.OrderDate);
            base.OrderDirection = "desc";
        }

        public OrderSpecification(string buyerEmail, int orderId) : base(p => p.BuyerEmail == buyerEmail && p.Id == orderId)
        {
            this.AddIncludes();
        }

        private void AddIncludes()
        {
            base.AddInclude(p => p.DeliveryMethod);
            base.AddInclude(p => p.Items);
        }
    }
}
