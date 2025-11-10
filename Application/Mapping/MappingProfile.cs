using AutoMapper;
using Domain.Entities.Core;
using Infrastructure.Data.Entities;
using Application.DTOs.Student;
using Application.DTOs.Teaher;
using Domain.Entities.Auth;
using Application.DTOs.Identity;


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
            CreateMap<StudentDomain, StudentDTO>().ReverseMap();
            CreateMap<StudentEntity, StudentDTO>();

            CreateMap<TeacherEntity, TeacherDomain>().ReverseMap();
            CreateMap<TeacherDomain, TeacherDTO>().ReverseMap();
            CreateMap<TeacherEntity, TeacherDTO>();

            CreateMap<LoginDTO, LoginDomain>();
            CreateMap<RegisterDTO, RegisterDomain>();
            CreateMap<RegisterDomain, ApplicationUserEntity>();

        }
    }
}
