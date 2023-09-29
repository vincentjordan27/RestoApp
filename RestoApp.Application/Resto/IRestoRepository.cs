using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Resto
{
    public interface IRestoRepository
    {
        Task<(List<Menu>, string?)> GetRestoMenu(Guid id);
        Task<string?> AddRestoMenu(Menu menu);
        Task<string?> UpdateRestoMenu(Menu menu);
    }
}
