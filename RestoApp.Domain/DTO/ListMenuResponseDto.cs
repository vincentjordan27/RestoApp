using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Domain.DTO
{
    public class ListMenuResponseDto
    {
        public string status;
        public string message;
        public List<MenuResponse> datas;
    }
}
