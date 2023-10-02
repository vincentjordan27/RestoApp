using AutoMapper;
using RestoApp.Domain.DTO;
using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRestoRequestDto, Domain.Entities.Resto>().ReverseMap();
            CreateMap<Menu, MenuResponse>().ReverseMap();
            CreateMap<MenuDto, Menu>().ReverseMap();
            CreateMap<Domain.Entities.Resto, RestoResponseDto>().ReverseMap();
        }
    }
}
