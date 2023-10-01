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

        public CustomerAuthService(UserManager<IdentityUser> userManager, ILogger<CustomerAuthService> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }
        public Task<string?> LoginCustomer(LoginRequestDto requestDto)
        {
            throw new NotImplementedException();
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

                }
            }
        }
    }
}
