using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Spesific;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services , IConfiguration configuration )
        {

            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(
                        configuration.GetConnectionString("MyConnection"),
                        b => b.MigrationsAssembly("Infrastructure"))
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
