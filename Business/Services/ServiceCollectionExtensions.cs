using AutoMapper;
using Business.Services.Auth;
using Data.UnitOfWork;
using Business.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Business.Services.Students;
using Data.Data.Entities;
using Business.Services.Teachers;

namespace Business.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IStudent, StudentService>();
            services.AddScoped<ITeacher, TeacherService>();

            services.AddAutoMapper(cfg => {
                cfg.AddProfile<MappingProfile>(); 
            }, typeof(ServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}