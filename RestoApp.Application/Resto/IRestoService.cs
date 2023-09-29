using RestoApp.Domain.DTO;
using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Resto
{
    public interface IRestoService
    {
        Task<ListMenuResponseDto> GetRestoMenu(Guid id);
        Task<GeneralResponse> AddMenu(MenuDto addMenu, Guid id);
        Task<GeneralResponse> UpdateMenu(MenuDto menuDto, Guid id, Guid userId);
    }
}
