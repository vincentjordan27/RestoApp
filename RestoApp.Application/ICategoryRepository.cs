using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application
{
    public interface ICategoryRepository
    {
        List<Domain.Category> GetAll();
    }
}
