using Domain.Entities.Core;
using Infrastructure.UnitOfWork;
using Infrastructure.Data.Entities;
using Application.DTOs.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Application.Validation;
using FluentValidation;
using Application.Results;

namespace Application.Services.Students
{
    public class StudentService : IStudent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentDTO> _validator;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<StudentDTO> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<Result<StudentDTO>> AddAsync(StudentDTO DTO)
        {


            // 1 DTO validation using FluentValidation
            var DtoValidationResult = await _validator.ValidateAsync(DTO);
            if (!DtoValidationResult.IsValid)
            {
                var errors = DtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<StudentDTO>(ErrorType.ValidationError, errors);
            }

            // 2 Map DTO to Domain
            var studentDomain = _mapper.Map<StudentDomain>(DTO);

            // 3️ Domain validation / Application rules
             var DomainValidationResult =  studentDomain.ValidateBusinessRules();
            if (!DomainValidationResult.IsSuccess)
            {
                return ResultFactory.Fail<StudentDTO>(ErrorType.ValidationError, DomainValidationResult.Errors);
            }

            // 4️ Map Domain to Entity and add to DB
            var studentEntity = _mapper.Map<StudentEntity>(studentDomain);

            try
            {
                await _unitOfWork.Students.AddAsync(studentEntity);

                // 5️ Save changes and check affected rows
                int affectedRows = await _unitOfWork.SaveChangesAsync();
                if (affectedRows == 0)
                {
                    return ResultFactory.Fail<StudentDTO>( ErrorType.Conflict, "Add operation failed. No rows affected.");
                }

                // 6️ Map saved entity back to DTO
                var savedDTO = _mapper.Map<StudentDTO>(studentEntity);

                return ResultFactory.Success(savedDTO);
            }
            catch (Exception ex)
            {
                // Log the erorr
                return ResultFactory.Fail<StudentDTO>(ErrorType.InternalError, "An unexpected database error occurred.");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int ID)
        {
            if (ID <= 0)
                return ResultFactory.Fail<bool>(ErrorType.ValidationError, "Invalid ID value.");

            // 1️ Find student by ID
            var studentToDelete = await _unitOfWork.Students.GetByIdAsync(ID);
            if (studentToDelete == null)
            {
                return ResultFactory.Fail<bool>( ErrorType.NotFound, $"Student with ID {ID} not found.");
            }

            try
            {
                // 2️ Delete student
                _unitOfWork.Students.Delete(studentToDelete);

                // 3️ Save changes and check affected rows
                int affectedRows = await _unitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return ResultFactory.Success(true);
                }
                else
                {
                    return ResultFactory.Fail<bool>(ErrorType.Conflict, $"Deletion failed. No rows affected for ID {ID}.");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<bool>(ErrorType.InternalError, "An unexpected database error occurred.");
            }
        }


        public async Task<Result<IEnumerable<StudentDTO>>> GetAllAsync()
        {
            try
            {
                var studentEntities = await _unitOfWork.Students.GetAllAsync();

                if (studentEntities == null || !studentEntities.Any())          
                    return ResultFactory.Success(Enumerable.Empty<StudentDTO>());
                

                // Map entities to DTOs
                var studentDTOs = _mapper.Map<IEnumerable<StudentDTO>>(studentEntities);

                //  Return success with mapped DTOs
                return ResultFactory.Success(studentDTOs);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<IEnumerable<StudentDTO>>( ErrorType.InternalError, "An unexpected database error occurred.");
            }
        }


        public async Task<Result<StudentDTO>> GetByIDAsync(int ID)
        {
            if (ID <= 0)
                return ResultFactory.Fail<StudentDTO>(ErrorType.ValidationError, "Invalid ID value.");

            try
            {
                // 1️ Find student by ID
                var studentEntity = await _unitOfWork.Students.GetByIdAsync(ID);

                //  Check if student exists
                if (studentEntity == null)
                    return ResultFactory.Fail<StudentDTO>(ErrorType.NotFound, $"Student with ID {ID} not found." );
                

                // 3️ Map entity to DTO
                var studentDTO = _mapper.Map<StudentDTO>(studentEntity);

                return ResultFactory.Success(studentDTO);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<StudentDTO>(ErrorType.InternalError, "An unexpected da tabase error occurred.");
            }
        }


        public async Task<Result<StudentDTO>> UpdateAsync(StudentDTO DTO)
        {
            // 1️ Find existing student by ID
            var existing = await _unitOfWork.Students.GetByIdAsync(DTO.Id);
            if (existing == null)     
                return ResultFactory.Fail<StudentDTO>(ErrorType.NotFound, "Student not found." );
            

            // 2️ DTO validation using FluentValidation
            var validationResult = await _validator.ValidateAsync(DTO);
            if (!validationResult.IsValid)        
                return ResultFactory.Fail<StudentDTO>(ErrorType.ValidationError, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            

            // 3️ Map DTO to Domain
            var studentDomain = _mapper.Map<StudentDomain>(DTO);

            // 4️ Domain validation / Application rules
            var DomainValidationResult = studentDomain.ValidateBusinessRules();
            if (!DomainValidationResult.IsSuccess)            
                return ResultFactory.Fail<StudentDTO>(ErrorType.ValidationError, DomainValidationResult.Errors);
            

            try
            {
                //// 5️ Map Domain to Entity and update in DB
                //var studentEntity = _mapper.Map<StudentEntity>(studentDomain);
                /*
                 
                     "Database error during update: The instance of entity type 'StudentEntity' cannot be tracked
                because another instance with the same key value for {'Id'} is already being tracked. 
                When attaching existing entities, ensure that only one entity instance with a given key value is attached.
                Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values."
                 
                 */
                _mapper.Map(studentDomain, existing);
                _unitOfWork.Students.Update(existing);

                // 6️ Save changes
                int affectedRows = await _unitOfWork.SaveChangesAsync();
                if (affectedRows == 0)
                {
                    return ResultFactory.Fail<StudentDTO>(ErrorType.Conflict, "Update failed. No corresponding student found or no changes detected." );
                }

                // 7️ Get updated entity and map to DTO
                var updated = await _unitOfWork.Students.GetByIdAsync(DTO.Id);
                var updatedDTO = _mapper.Map<StudentDTO>(updated);

                // 8️ Return success
                return ResultFactory.Success(updatedDTO);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<StudentDTO>( ErrorType.InternalError, "An unexpected database error occurred.");
            }
        }
    }
}