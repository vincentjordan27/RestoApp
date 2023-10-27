using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid RestoId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set;}
    }
}
