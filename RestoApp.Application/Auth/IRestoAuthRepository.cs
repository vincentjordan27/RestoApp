using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public interface IRestoAuthRepository
    {
        Task<string?> InsertResto(Resto resto);
    }
}
