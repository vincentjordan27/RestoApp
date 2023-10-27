using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Domain.DTO
{

    public class CreateOrderDto
    {
        public Guid RestoId { get; set; }
        public Guid StatusId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
    }
}
