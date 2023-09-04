using RestoApp.Application;
using RestoApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        public static List<Category> list = new List<Category>()
        {
            new Category { Id = Guid.NewGuid(), Name = "Test1"},
            new Category { Id = Guid.NewGuid(), Name = "Test2"},
            new Category { Id = Guid.NewGuid(), Name = "Test3"},
            new Category { Id = Guid.NewGuid(), Name = "Test4"},
            new Category { Id = Guid.NewGuid(), Name = "Test5"},
            new Category { Id = Guid.NewGuid(), Name = "Test6"},
        };
        public List<Category> GetAll()
        {
            return list;
        }
            
    }
}
