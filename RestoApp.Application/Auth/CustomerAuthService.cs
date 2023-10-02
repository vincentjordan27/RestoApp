using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RestoApp.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public class CustomerAuthService : ICustomerAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<CustomerAuthService> logger;
        private readonly ITokenRepository tokenRepository;

        public CustomerAuthService(UserManager<IdentityUser> userManager, ILogger<CustomerAuthService> logger, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.tokenRepository = tokenRepository;
        }
        public async Task<string?> LoginCustomer(LoginRequestDto requestDto)
        {
            var user = await userManager.FindByNameAsync(requestDto.Username);
            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, requestDto.Password);
                if (result)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        if (roles[0] == "Customer")
                        {
                            var jwtToken = tokenRepository.GetToken(user, roles.ToList());
                            return jwtToken;
                        } else
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<string?> RegisterCustomer(RegisterCustomerRequestDto requestDto)
        {
            var id = Guid.NewGuid();
            var identityUser = new IdentityUser
            {
                UserName = requestDto.Name,
                Id = id.ToString(),
            };
            var identityResult = await userManager.CreateAsync(identityUser, requestDto.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(identityUser, "Customer");
                if (identityResult.Succeeded)
                {
                    return null;
                }
                logger.LogError("Error Addtorole Register Customer");
                return identityResult.Errors.ToList()[0].Description;
            }
            return identityResult.Errors.ToList()[0].Description;
        }
    }
}
