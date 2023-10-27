using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }
        public Guid MenuId { get; set; }
        public string Menu { get; set; }
    }
}
