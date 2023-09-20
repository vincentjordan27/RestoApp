using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestoApp.Domain.DTO;
using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Resto
{
    public class RestoService : IRestoService
    {
        private readonly ILogger<RestoService> logger;
        private readonly IMapper mapper;
        private readonly IRestoRepository restoRepository;

        public RestoService(ILogger<RestoService> logger, IMapper mapper, IRestoRepository restoRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.restoRepository = restoRepository;
        }

        public async Task<ListMenuResponseDto> GetRestoMenu(Guid id)
        {
            var listMenu = await restoRepository.GetRestoMenu(id);
            if (listMenu.Item2 != null)
            {
                var responseError = new ListMenuResponseDto
                {
                    message = listMenu.Item2,
                    status = "Error",
                };
                return responseError;
            }
            var listMenuDomain = new List<Menu>(listMenu.Item1);
            var dataMenu = mapper.Map<List<MenuResponse>>(listMenuDomain);
            var response = new ListMenuResponseDto
            {
                datas = dataMenu,
                status = "Success",
            };
            return response;
        }
    }
}
