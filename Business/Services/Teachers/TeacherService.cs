using AutoMapper;
using Domain.Entities.Core;
using Business.DTOs.Student;
using Business.Results;
using Business.DTOs.Teaher;
using Data.Data.Entities;
using Data.UnitOfWork;
using FluentValidation;

namespace Business.Services.Teachers
{
    public class TeacherService : ITeacher
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<TeacherDTO> _validator;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TeacherDTO> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<Result<TeacherDTO>> AddAsync(TeacherDTO teacherDTO)
        {
            // 1️ DTO validation
            var dtoValidationResult = await _validator.ValidateAsync(teacherDTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<TeacherDTO>(errors);
            }

            // 2️ Map to domain & Domain validation
            var teacherDomain = _mapper.Map<TeacherDomain>(teacherDTO);
            var domainValidationResult = teacherDomain.ValidateBusinessRules();
            if (!domainValidationResult.IsSuccess)
            {
                return ResultFactory.Fail<TeacherDTO>(domainValidationResult.Errors);
            }

            // 3️  Map to entity and save
            var teacherEntity = _mapper.Map<TeacherEntity>(teacherDomain);
            try
            {
                await _unitOfWork.Teachers.AddAsync(teacherEntity);
                var affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    return ResultFactory.Fail<TeacherDTO>("Failed to save the teacher to the database.");
                }

                var savedDTO = _mapper.Map<TeacherDTO>(teacherEntity);
                return ResultFactory.Success(savedDTO);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<TeacherDTO>("An unexpected database error occurred: " + ex.Message);
            }

        }

        public async Task<Result<bool>> DeleteAsync(int ID)
        {
            // 1️ Try to get the teacher entity by ID
            var teacherToDelete = await _unitOfWork.Teachers.GetByIdAsync(ID);

            if (teacherToDelete == null)
            {
                return ResultFactory.Fail<bool>($"Teacher with ID {ID} not found.");
            }

            try
            {
                // 2️ Mark the entity for deletion
                _unitOfWork.Teachers.Delete(teacherToDelete);

                // 3️ Save changes and get the number of affected rows
                int affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    // Deletion successful
                    return ResultFactory.Success(true);
                }
                else
                {
                    // Deletion failed without exception
                    return ResultFactory.Fail<bool>($"Deletion failed. No rows affected for ID {ID}.");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<bool>($"Database error during deletion: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TeacherDTO>>> GetAllAsync()
        {
            try
            {
                // 1️ Retrieve all teacher entities from the database
                var teacherEntities = await _unitOfWork.Teachers.GetAllAsync();

                // 2️ If no teachers found, return empty list as success
                if (teacherEntities == null || !teacherEntities.Any())
                {
                    return ResultFactory.Success(Enumerable.Empty<TeacherDTO>());
                }

                // 3️ Map entities to DTOs
                var teacherDTOs = _mapper.Map<IEnumerable<TeacherDTO>>(teacherEntities);

                // 4️ Return successful result with data
                return ResultFactory.Success(teacherDTOs);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<IEnumerable<TeacherDTO>>($"An error occurred while retrieving teachers: {ex.Message}");
            }
        }

        public async Task<Result<TeacherDTO>> GetByIDAsync(int id)
        {
            try
            {
                var teacherEntity = await _unitOfWork.Teachers.GetByIdAsync(id);

                //  If teacher not found, return failure result
                if (teacherEntity == null)
                {
                    return ResultFactory.Fail<TeacherDTO>($"Teacher with ID {id} not found.");
                }

                //  Map entity to DTO
                var teacherDTO = _mapper.Map<TeacherDTO>(teacherEntity);

                return ResultFactory.Success(teacherDTO);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<TeacherDTO>($"An error occurred while retrieving teacher: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TeacherDTO>>> GetTeachersByDepartmentAsync(int departmentId)
        {
            try
            {
                // 1️ Retrieve all teacher entities for the given department
                var teacherEntities = await _unitOfWork.Teachers.GetTeachersByDepartmentAsync(departmentId);

                if (teacherEntities == null || !teacherEntities.Any())
                {
                    return ResultFactory.Success(Enumerable.Empty<TeacherDTO>());
                }

                // 2 Map entities to DTOs
                var teacherDTOs = _mapper.Map<IEnumerable<TeacherDTO>>(teacherEntities);

                return ResultFactory.Success(teacherDTOs);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<IEnumerable<TeacherDTO>>(
                    $"An error occurred while retrieving teachers for department {departmentId}: {ex.Message}"
                );
            }
        }

        public async Task<Result<TeacherDTO>> UpdateAsync(TeacherDTO DTO)
        {
            // 1️ Check if teacher exists in the database
            var existing = await _unitOfWork.Teachers.GetByIdAsync(DTO.Id);
            if (existing == null)
                return ResultFactory.Fail<TeacherDTO>(new List<string> { "Teacher not found." });


            // 2 DTO validation using FluentValidation
            var validationResult = await _validator.ValidateAsync(DTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<TeacherDTO>(errors);
            }

            // 3 Map DTO to Domain
            var teacherDomain = _mapper.Map<TeacherDomain>(DTO);

            // 4️ Business rules validation inside Domain

            var domainValidationResult = teacherDomain.ValidateBusinessRules();
            if (!domainValidationResult.IsSuccess)
            {
                return ResultFactory.Fail<TeacherDTO>(domainValidationResult.Errors);
            }

            try
            {
                //// 5️ Map Domain to Entity and update
                //var teacherEntity = _mapper.Map<TeacherEntity>(teacherDomain);
                /*

     "Database error during update: The instance of entity type 'StudentEntity' cannot be tracked
because another instance with the same key value for {'Id'} is already being tracked. 
When attaching existing entities, ensure that only one entity instance with a given key value is attached.
Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values."

 */

                _mapper.Map(teacherDomain, existing);
                _unitOfWork.Teachers.Update(existing);

                // 6️ Save changes
                int affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows == 0)
                    return ResultFactory.Fail<TeacherDTO>("Update failed. No corresponding teacher found or no changes detected.");
                
                // 7️ Retrieve the updatedEntity entity to return fresh data
                var updatedEntity = await _unitOfWork.Teachers.GetByIdAsync(DTO.Id);
                var updatedDTO = _mapper.Map<TeacherDTO>(updatedEntity);

                // 8️ Return success with updatedEntity DTO
                return ResultFactory.Success(updatedDTO);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<TeacherDTO>(new List<string> { $"Database error: {ex.Message}" });
            }
        }
    }
}
