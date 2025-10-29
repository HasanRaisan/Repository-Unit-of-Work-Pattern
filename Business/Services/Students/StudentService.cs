using Business.Domains.Core;
using Data.UnitOfWork;
using Data.Data.Entities;
using Business.DTOs.Student;
using AutoMapper;
using Business.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Business.Validation;
using FluentValidation;

namespace Business.Services.Students
{
    public class StudentService : IStudent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentDomain> _validator;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<StudentDomain> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<Result<StudentDTO>> AddAsync(StudentDTO DTO)
        {
            var studentDomain = _mapper.Map<StudentDomain>(DTO);

            var validationResult = await _validator.ValidateAsync(studentDomain);

            if (!validationResult.IsValid)
            {
                return new Result<StudentDTO>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var studentEntity = _mapper.Map<StudentEntity>(studentDomain);
            try
            {
                await _unitOfWork.Students.AddAsync(studentEntity);
                await _unitOfWork.SaveChangesAsync();

                var savedDTO = _mapper.Map<StudentDTO>(studentEntity);
                return new Result<StudentDTO>()
                {
                    IsSuccess = true,
                    Data = savedDTO
                };
            }
            catch (Exception ex)
            {
                return new Result<StudentDTO>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "An unexpected database error occurred." }
                };
            }
        }
        

        public Result<StudentDTO> Delete(StudentDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<StudentDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<StudentDTO>> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Result<StudentDTO> Update(StudentDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}