using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestoApp.Domain.Constant;
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
        private readonly IOrderRepository orderRepository;

        public RestoService(ILogger<RestoService> logger, IMapper mapper, IRestoRepository restoRepository, IOrderRepository orderRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.restoRepository = restoRepository;
            this.orderRepository = orderRepository;
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

        public async Task<GeneralResponse> AddMenu(MenuDto addMenu, Guid id)
        {
            var dataMenu = mapper.Map<Menu>(addMenu);
            dataMenu.Id = Guid.NewGuid();
            dataMenu.RestoId = id;
            var result = await restoRepository.AddRestoMenu(dataMenu);
            var response = new GeneralResponse { };
            if (result == null)
            {
                response.Status = Constant.SUCCESS;
                response.Message = "Success Add New Menu";
            } else
            {
                response.Status = Constant.ERROR;
                response.Message = result;
            }
            return response;
        }

        public async Task<GeneralResponse> UpdateMenu(MenuDto menuDto, Guid id, Guid userId)
        {
            var dataMenu = mapper.Map<Menu>(menuDto);
            dataMenu.Id = id;
            dataMenu.RestoId = userId;
            var result = await restoRepository.UpdateRestoMenu(dataMenu);
            var response = new GeneralResponse { };
            if (result == null)
            {
                response.Status = Constant.SUCCESS;
                response.Message = "Success Update Menu";
            }
            else
            {
                response.Status = Constant.ERROR;
                response.Message = result;
            }
            return response;
        }

        public async Task<GeneralResponse> DeleteMenu(Guid id, Guid userId)
        {
            var result = await restoRepository.DeleteRestoMenu(id, userId);
            var response = new GeneralResponse { }; 
            if (result == null)
            {
                response.Status = Constant.SUCCESS;
                response.Message = "Success Delete Menu";
            } else
            {
                response.Status = Constant.ERROR;
                response.Message = result;
            }
            return response;
        }

        public async Task<ListRestoResponseDto> GetListResto()
        {
            var result = await restoRepository.GetResto();
            var response = new ListRestoResponseDto();
            if (result.Item2 != null)
            {
                response.Status = Constant.ERROR;
                response.Message = result.Item2;
                return response;
            }
            var listItem = mapper.Map<List<RestoResponseDto>>(result.Item1);
            response.Status = Constant.SUCCESS;
            response.Message = "Success Get Resto List";
            response.Datas = listItem;
            return response;
        }

        public async Task<GeneralResponse> OrderMenu(CreateOrderDto orderDto, Guid userId)
        {
            var order = mapper.Map<Order>(orderDto);
            order.CustomerId = userId;
            order.Id = Guid.NewGuid();
            var result = await orderRepository.OrderMenu(order);
            var response = new GeneralResponse { };
            if (result == null)
            {
                response.Status = Constant.SUCCESS;
                response.Message = "Success Order Menu";
            }
            else
            {
                response.Status = Constant.ERROR;
                response.Message = result;
            }
            return response;
        }
    }
}
