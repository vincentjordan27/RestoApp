using RestoApp.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public interface ICustomerAuthService
    {
        Task<string?> RegisterCustomer(RegisterCustomerRequestDto requestDto);
        Task<string?> LoginCustomer(LoginRequestDto requestDto);
    }
}
