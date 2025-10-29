using AutoMapper;
using Business.Domains.Core;
using Data.Data.Entities;
using Business.DTOs.Student;
using Clean_Three_Tier_First.DTOs.Teaher;


namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*
             Mapping Operation: 
                1 -   Entity (Data Access) => Domain (Business) => DTO (API/Presintation) 

                2 -   DTO (API/Presintation) => Domain (Business) => Entity (Data Access)
             */

            CreateMap<StudentEntity, StudentDomain>().ReverseMap();
            CreateMap<StudentDomain, StudentDTO>().ReverseMap();
            CreateMap<StudentEntity, StudentDTO>();

            CreateMap<TeacherEntity, TeacherDomain>().ReverseMap();
            CreateMap<TeacherDomain, TeacherDTO>().ReverseMap();
            
        }
    }
}
