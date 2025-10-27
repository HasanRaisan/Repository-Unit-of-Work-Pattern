using Business.Domains.Core;
using Clean_Three_Tier_First.DTOs.Teaher;
using Data.UnitOfWork;

namespace Business.Services.Teachers
{
    public class TeacherService : ITeacher
    {
        IUnitOfWork _unitOfWork;
        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(TeacherDTO DTO)
        {
            throw new NotImplementedException();
        }

        public void Delete(TeacherDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeacherDTO>> GetAllAsync() 
        {
            throw new NotImplementedException();
        }

        public Task<TeacherDTO?> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeacherDTO>> GetTeachersByDepartmentAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(TeacherDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
