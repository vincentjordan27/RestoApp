using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Infrastructure.Data
{
    public  class RestoAuthDbContext : IdentityDbContext
    {
        public RestoAuthDbContext(DbContextOptions<RestoAuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            var restoRoleId = "c658ac3d-bb18-4a46-a9d5-610774a21c62";
            var customerRoleId = "d193a029-36ab-478b-bf89-3302bfb34f53";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = restoRoleId,
                    ConcurrencyStamp = restoRoleId,
                    Name = "Resto",
                    NormalizedName = "Resto".ToUpper(),
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper(),
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
