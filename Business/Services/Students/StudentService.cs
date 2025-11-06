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

            // validation
            var validationResult = await _validator.ValidateAsync(studentDomain);

            if (!validationResult.IsValid)
            {
                return new Result<StudentDTO>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            // add
            var studentEntity = _mapper.Map<StudentEntity>(studentDomain);
            try
            {
                await _unitOfWork.Students.AddAsync(studentEntity);
                await _unitOfWork.SaveChangesAsync(); // don't we need to check affected number ?? not just in update and delete (soft failures)

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
        public async Task<Result<bool>> DeleteAsync(int ID)
        {
            var studentToDelete = await _unitOfWork.Students.GetByIdAsync(ID);

            if (studentToDelete == null)
            {
                return new Result<bool>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Student with ID {ID} not found." }
                };
            }

            try
            {
                _unitOfWork.Students.Delete(studentToDelete);

                int affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    return new Result<bool>
                    {
                        IsSuccess = true,
                        Data = true
                    };
                }
                else
                {
                    return new Result<bool>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"Deletion failed. No rows affected for ID {ID}." }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Database error during deletion: {ex.Message}" }
                };
            }
        }

        public async Task<Result<IEnumerable<StudentDTO>>> GetAllAsync()
        {
            try
            {
                var studentEntities = await _unitOfWork.Students.GetAllAsync();
                if(studentEntities == null || !studentEntities.Any())
                {
                    return new Result<IEnumerable<StudentDTO>>
                    {
                        IsSuccess = true,
                        Data = Enumerable.Empty<StudentDTO>()
                    };
                }

                var studentDTOs = _mapper.Map<IEnumerable<StudentDTO>>(studentEntities);
                return new Result<IEnumerable<StudentDTO>>
                {
                    IsSuccess = true,
                    Data = studentDTOs
                };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<StudentDTO>>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"An error occurred while retrieving students: {ex.Message}" }
                };
            }
        }

        public async Task<Result<StudentDTO>> GetByIDAsync(int id)
        {
            try
            {
                var studentEntity = await _unitOfWork.Students.GetByIdAsync(id);

                if (studentEntity == null)
                {
                    return new Result<StudentDTO>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"Student with ID {id} not found." }
                    };
                }

                var studentDTO = _mapper.Map<StudentDTO>(studentEntity);

                return new Result<StudentDTO>
                {
                    IsSuccess = true,
                    Data = studentDTO
                };
            }
            catch (Exception ex)
            {
                return new Result<StudentDTO>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"An error occurred while retrieving student: {ex.Message}" }
                };
            }
        }

        public async Task<Result<StudentDTO>> UpdateAsync(StudentDTO DTO)
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

            try
            {
                var studentEntity = _mapper.Map<StudentEntity>(studentDomain);
                _unitOfWork.Students.Update(studentEntity);

                int affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    return new Result<StudentDTO>
                    {
                        IsSuccess = true,
                        Data = DTO
                    };
                }
                else
                {
                    return new Result<StudentDTO>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Update failed. No corresponding student found or no changes detected." }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<StudentDTO>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Database error during update: {ex.Message}" }
                };
            }
        }
    }
}