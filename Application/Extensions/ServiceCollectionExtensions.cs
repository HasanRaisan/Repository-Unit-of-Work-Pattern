using Application.Configruration;
using Application.Mapping;
using Application.Services.Auth;
using Application.Services.Departments;
using Application.Services.Logging;
using Application.Services.Students;
using Application.Services.Teachers;
using Application.Validation.Teacher;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, typeof(ServiceCollectionExtensions).Assembly);


            services.AddValidatorsFromAssemblyContaining<TeacherCreateDTOValidator>();

            services.AddScoped<IErrorLogService, ErrorLogService>();

            return services;
        }
    }
}