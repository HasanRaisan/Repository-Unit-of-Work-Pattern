using Application.Configruration;
using Application.Mapping;
using Application.Services.Auth;
using Application.Services.Students;
using Application.Services.Teachers;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Spesific;
using Infrastructure.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Application.Validation.Teacher;
using Application.Services.Departments;


namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IStudent, StudentService>();
            services.AddScoped<ITeacher, TeacherService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, typeof(ServiceCollectionExtensions).Assembly);


            services.AddValidatorsFromAssemblyContaining<TeacherCreateDTOValidator>();


            return services;
        }
    }
}