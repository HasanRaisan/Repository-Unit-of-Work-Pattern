using Business.Domains.Core;
using Business.Result;
using Clean_Three_Tier_First.DTOs.Teaher;
using Data.UnitOfWork;

namespace Business.Services.Teachers
{
    public class TeacherService : ITeacher
    {
        public Task<Result<TeacherDTO>> AddAsync(TeacherDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Result<TeacherDTO> Delete(TeacherDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<TeacherDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<TeacherDTO>> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeacherDTO>> GetTeachersByDepartmentAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Result<TeacherDTO> Update(TeacherDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
