using Application.Configruration;
using Application.Mapping;
using Application.Services.Auth;
using Application.Services.Students;
using Application.Services.Teachers;
using Application.Validation;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Spesific;
using Infrastructure.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITeacherRepository, TeacherRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IStudent, StudentService>();
            services.AddScoped<ITeacher, TeacherService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, typeof(ServiceCollectionExtensions).Assembly);


            services.AddValidatorsFromAssemblyContaining<StudentDTOValidator>();


            return services;
        }
    }
}