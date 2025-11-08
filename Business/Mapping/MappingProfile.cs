using AutoMapper;
using Domain.Entities.Core;
using Data.Data.Entities;
using Business.DTOs.Student;
using Business.DTOs.Teaher;
using Domain.Entities.Auth;
using Business.DTOs.Identity;


namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*
             Mapping Operation: 
                1 -   Entity (Data Access) => Entities (Business) => DTO (API/Presintation) 

                2 -   DTO (API/Presintation) => Entities (Business) => Entity (Data Access)
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
