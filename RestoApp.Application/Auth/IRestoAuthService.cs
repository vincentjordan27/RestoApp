using RestoApp.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public interface IRestoAuthService
    {
        Task<string?> RegisterResto(RegisterRestoRequestDto registerRestoDTO);
        Task<string?> LoginResto(LoginRequestDto loginRestoDTO);
        
    }
}
