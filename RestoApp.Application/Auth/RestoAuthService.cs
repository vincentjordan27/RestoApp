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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestoApp.Application.Auth
{
    public class RestoAuthService : IRestoAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<RestoAuthService> logger;
        private readonly IMapper mapper;
        private readonly IRestoAuthRepository restoAuthRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITokenRepository tokenRepository;

        public RestoAuthService(UserManager<IdentityUser> userManager, ILogger<RestoAuthService> logger, 
            IMapper mapper, IRestoAuthRepository restoAuthRepository, ICategoryRepository categoryRepository,
            ITokenRepository tokenRepository
        )
        {
            this.userManager = userManager;
            this.logger = logger;
            this.mapper = mapper;
            this.restoAuthRepository = restoAuthRepository;
            this.categoryRepository = categoryRepository;
            this.tokenRepository = tokenRepository;
        }

        public async Task<string?> LoginResto(LoginRequestDto loginRestoDTO)
        {
            var user = await userManager.FindByNameAsync(loginRestoDTO.Username);
            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, loginRestoDTO.Password);
                if (result)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.GetToken(user, roles.ToList());
                        return jwtToken;
                    }
                }
            }
            return null;
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
                identityResult = await userManager.AddToRoleAsync(identityUser, "Resto");
                if (identityResult.Succeeded)
                {
                    var resto = mapper.Map<Resto>(registerRestoDTO);
                    resto.Id = id;
                    var resultInsert = await restoAuthRepository.InsertResto(resto);
                    logger.LogError(resultInsert);
                    return resultInsert;
                }
                logger.LogError("Error Addtorole");
                return identityResult.Errors.ToList()[0].Description;
            }
            return identityResult.Errors.ToList()[0].Description;
        }
    }
}
