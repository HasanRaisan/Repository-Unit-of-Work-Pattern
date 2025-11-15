using Application.DTOs.Department;
using Application.DTOs.Identity;
using Application.DTOs.Student;
using Application.DTOs.Teaher;
using AutoMapper;
using Domain.Entities.Auth.Login;
using Domain.Entities.Auth.Register;
using Domain.Entities.Department;
using Domain.Entities.Student;
using Domain.Entities.Teacher;
using Infrastructure.Data.Entities;


namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*
             Mapping Operation: 
                1 -   Entity (Data Access) => Entities (Application) => DTO (API/Presintation) 

                2 -   DTO (API/Presintation) => Entities (Application) => Entity (Data Access)
             */

            CreateMap<StudentEntity, StudentDomain>().ReverseMap();
            CreateMap<StudentEntity, StudentDTO>();

            CreateMap<TeacherEntity, TeacherDomain>().ReverseMap();
            CreateMap<TeacherEntity, TeacherDTO>();

            CreateMap<LoginDTO, LoginDomain>();
            CreateMap<RegisterDTO, RegisterDomain>();
            CreateMap<RegisterDomain, ApplicationUserEntity>();

            CreateMap<DepartmentDomain, DepartmentEntity>().ReverseMap();

            CreateMap<DepartmentEntity, DepartmentDTO>();

        }
    }
}
