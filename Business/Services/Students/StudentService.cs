using Business.Domains.Core;
using Data.UnitOfWork;
using Data.Data.Entities;
using Business.DTOs.Student;

namespace Business.Services.Students
{
    public class StudentService : IStudent
    {
        IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(StudentDTO DTO)
        {
            throw new NotImplementedException();

            //_unitOfWork.Students.Update(new Student());

        }
        public void Delete(StudentDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO?> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(StudentDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
