using Data.Data;
using Data.Data.Entities;
using Data.Repositories.Generic;
using Data.Repositories.Spesific;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services , IConfiguration configuration )
        {

            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(
                        configuration.GetConnectionString("MyConnection"),
                        b => b.MigrationsAssembly("Data"))
            );



            services.AddIdentity<ApplicationUserEntity, ApplicationRoleEntity>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
