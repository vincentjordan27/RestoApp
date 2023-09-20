using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public interface ITokenRepository
    {
        string GetToken(IdentityUser identityUser, List<string> roles);
        Guid GetUserId(string token);
    }
}
