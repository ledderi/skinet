using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.Items = new Collection<OrderItem>();
        }

        public ICollection<OrderItem> Items { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string BuyerEmail { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal  GetSubTotal()
        {
            return this.Items.Sum(p => p.Quantity * p.Price);
        }

        public decimal GetTotal()
        {
            return this.GetSubTotal() + this.DeliveryMethod.Price;
        }
    }
}
