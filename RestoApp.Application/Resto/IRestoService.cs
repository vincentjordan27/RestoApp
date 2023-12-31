﻿using RestoApp.Domain.DTO;
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
        Task<ListRestoResponseDto> GetListResto();
        Task<ListMenuResponseDto> GetRestoMenu(Guid id);
        Task<GeneralResponse> AddMenu(MenuDto addMenu, Guid id);
        Task<GeneralResponse> UpdateMenu(MenuDto menuDto, Guid id, Guid userId);
        Task<GeneralResponse> DeleteMenu(Guid id, Guid userId);
        Task<GeneralResponse> OrderMenu(CreateOrderDto orderDto, Guid userId);
    }
}
