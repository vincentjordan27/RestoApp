using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RestoApp.Domain.DTO;
using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public class RestoAuthService : IRestoAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<RestoAuthService> logger;
        private readonly IMapper mapper;
        private readonly IRestoAuthRepository restoAuthRepository;
        private readonly ICategoryRepository categoryRepository;

        public RestoAuthService(UserManager<IdentityUser> userManager, ILogger<RestoAuthService> logger, IMapper mapper, IRestoAuthRepository restoAuthRepository, ICategoryRepository categoryRepository)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.mapper = mapper;
            this.restoAuthRepository = restoAuthRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<string?> RegisterResto(RegisterRestoRequestDto registerRestoDTO)
        {
            var result = await categoryRepository.CategoryExist(registerRestoDTO.CategoryId);
            if (result == null)
            {
                return "Terjadi Kesalahan";
            }
            else if((bool)!result)
            {
                return "Category Tidak Terdaftar";
            }
            var id = Guid.NewGuid();
            var identityUser = new IdentityUser
            {
                UserName = registerRestoDTO.Username,
                Id = id.ToString(),
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRestoDTO.Password);
            if (identityResult.Succeeded)
            {
                var resto = mapper.Map<Resto>(registerRestoDTO);
                resto.Id = id;
                var resultInsert = await restoAuthRepository.InsertResto(resto);
                return resultInsert;
            }
            
            return identityResult.Errors.ToList()[0].Description;
        }
    }
}
