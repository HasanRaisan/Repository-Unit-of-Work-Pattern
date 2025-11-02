using Business.Domains.Core;
using Business.Mapping;
using Business.Services.Auth;
using Business.Services.Students;
using Business.Services.Teachers;
using Business.Validation;
using Data.Repositories.Generic;
using Data.Repositories.Spesific;
using Data.UnitOfWork;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITeacherRepository, TeacherRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IStudent, StudentService>();
            services.AddScoped<ITeacher, TeacherService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, typeof(ServiceCollectionExtensions).Assembly);


            services.AddValidatorsFromAssemblyContaining<StudentDomainValidator>();



            return services;
        }
    }
}