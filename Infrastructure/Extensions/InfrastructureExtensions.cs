using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Spesific;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services , IConfiguration configuration )
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITeacherRepository, TeacherRepository>();

            services.AddScoped<IUnitOfWork, Infrastructure.UnitOfWork.UnitOfWork>();


            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(
                        configuration.GetConnectionString("MyConnection"),
                        b => b.MigrationsAssembly("Infrastructure"))
                        .UseLazyLoadingProxies()
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
