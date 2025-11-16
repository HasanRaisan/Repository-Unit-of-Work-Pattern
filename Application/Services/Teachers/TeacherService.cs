using AutoMapper;
using Application.DTOs.Teaher;
using Infrastructure.Data.Entities;
using Infrastructure.UnitOfWork;
using FluentValidation;
using Application.Results;
using Domain.Entities.Teacher;
using Application.Services.Logging;
using System.Reflection;

namespace Application.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<TeacherCreateDTO> _teacherCreateDTOValidator;
        private readonly IValidator<TeacherUpdateDTO> _teacherUpdateDTOValidator;
        private readonly IErrorLogService _logger;
        private const string DbErrorMessage = "An unexpected database error occurred.";
        private const string PathForErrorLog = "TeacherSerivce";
        private Result<IEnumerable<TeacherDTO>> EmptyList() => ResultFactory.Success(Enumerable.Empty<TeacherDTO>());
        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<TeacherCreateDTO> TeacherCreateDTOValidator,
            IValidator<TeacherUpdateDTO> teacherUpdateDTOValidator,
            IErrorLogService logger)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._teacherCreateDTOValidator = TeacherCreateDTOValidator;
            this._teacherUpdateDTOValidator = teacherUpdateDTOValidator;
            this._logger = logger;
        }

        public async Task<Result<TeacherDTO>> AddAsync(TeacherCreateDTO DTO)
        {
            // 1️ DTO validation
            var dtoValidationResult = await _teacherCreateDTOValidator.ValidateAsync(DTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<TeacherDTO>(ErrorType.ValidationError, errors);
            }

            // 2️ Map to domain & Domain validation
            var domainValidationResult = TeacherDomain.Create(DTO.Name, DTO.Subject,DTO.Salary, DTO.DepartmentId);
            if (!domainValidationResult.IsSuccess)
                return ResultFactory.Fail<TeacherDTO>(ErrorType.ValidationError, domainValidationResult.Errors);

            var teacherDomain = domainValidationResult.Data;

            
            var teacherEntity = _mapper.Map<TeacherEntity>(teacherDomain);
            try
            {
                await _unitOfWork.Teachers.AddAsync(teacherEntity);
                var affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    return ResultFactory.Fail<TeacherDTO>(ErrorType.Conflict, "Failed to save the teacher to the database.");
                }

                var savedDTO = _mapper.Map<TeacherDTO>(teacherEntity);
                return ResultFactory.Success(savedDTO);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method:nameof(AddAsync));

                return ResultFactory.Fail<TeacherDTO>(ErrorType.InternalError, DbErrorMessage);
            }

        }
        public async Task<Result<bool>> DeleteAsync(int ID)
        {
            if (ID <= 0)
                return ResultFactory.Fail<bool>(ErrorType.ValidationError, "Invalid ID value.");

            // 1️ Try to get the teacher entity by ID
            var teacherToDelete = await _unitOfWork.Teachers.GetByIdAsync(ID);

            if (teacherToDelete == null)
            {
                return ResultFactory.Fail<bool>(ErrorType.NotFound, $"Teacher with ID {ID} not found.");
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
                    return ResultFactory.Fail<bool>(ErrorType.Conflict, $"Deletion failed. No rows affected for ID {ID}.");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(DeleteAsync));

                return ResultFactory.Fail<bool>(ErrorType.InternalError, DbErrorMessage);
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
                    return EmptyList();
                }

                // 3️ Map entities to DTOs
                var teacherDTOs = _mapper.Map<IEnumerable<TeacherDTO>>(teacherEntities);

                // 4️ Return successful result with data
                return ResultFactory.Success(teacherDTOs);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(GetAllAsync));

                return ResultFactory.Fail<IEnumerable<TeacherDTO>>(ErrorType.InternalError, DbErrorMessage);
            }
        }
        public async Task<Result<TeacherDTO>> GetByIDAsync(int ID)
        {
            if (ID <= 0)
                return ResultFactory.Fail<TeacherDTO>(ErrorType.ValidationError, "Invalid ID value.");

            try
            {
                var teacherEntity = await _unitOfWork.Teachers.GetByIdAsync(ID);

                //  If teacher not found, return failure result
                if (teacherEntity == null)
                {
                    return ResultFactory.Fail<TeacherDTO>(ErrorType.NotFound, $"Teacher with ID {ID} not found.");
                }

                //  Map entity to DTO
                var teacherDTO = _mapper.Map<TeacherDTO>(teacherEntity);

                return ResultFactory.Success(teacherDTO);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(GetByIDAsync));

                return ResultFactory.Fail<TeacherDTO>(ErrorType.InternalError, DbErrorMessage);
            }
        }
        public async Task<Result<IEnumerable<TeacherDTO>>> GetTeachersByDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0)
                return ResultFactory.Fail<IEnumerable<TeacherDTO >> (ErrorType.ValidationError, "Invalid ID value.");
            try
            {
                // 1️ Retrieve all teacher entities for the given department
                var teacherEntities = await _unitOfWork.Teachers.GetTeachersByDepartmentAsync(departmentId);

                if (teacherEntities == null || !teacherEntities.Any())
                {
                    return EmptyList();
                }

                // 2 Map entities to DTOs
                var teacherDTOs = _mapper.Map<IEnumerable<TeacherDTO>>(teacherEntities);

                return ResultFactory.Success(teacherDTOs);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: "GetTeachersByDepartmentAsync");

                return ResultFactory.Fail<IEnumerable<TeacherDTO>>(ErrorType.InternalError, DbErrorMessage);
            }
        }
        public async Task<Result<TeacherDTO>> UpdateAsync(TeacherUpdateDTO DTO)
        {
            // 1️ Check if teacher exists in the database
            var existing = await _unitOfWork.Teachers.GetByIdAsync(DTO.Id);
            if (existing == null)
                return ResultFactory.Fail<TeacherDTO>(ErrorType.NotFound, "Teacher not found.");


            // 2 DTO validation using FluentValidation
            var validationResult = await _teacherUpdateDTOValidator.ValidateAsync(DTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<TeacherDTO>(ErrorType.ValidationError, errors);
            }


            // 4️ Application rules validation inside Domain
            var domainValidationResult = TeacherDomain.Update(DTO.Id, DTO.Name, DTO.Subject, DTO.Salary, DTO.DepartmentId);
            if (!domainValidationResult.IsSuccess)
                return ResultFactory.Fail<TeacherDTO>(ErrorType.ValidationError, domainValidationResult.Errors);

            var teacherDomain = domainValidationResult.Data;


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
                    return ResultFactory.Fail<TeacherDTO>(ErrorType.Conflict, "Update failed. No rows affected.");
                
                // 7️ Retrieve the updatedEntity entity to return fresh data
                var updatedEntity = await _unitOfWork.Teachers.GetByIdAsync(DTO.Id);
                var updatedDTO = _mapper.Map<TeacherDTO>(updatedEntity);

                // 8️ Return success with updatedEntity DTO
                return ResultFactory.Success(updatedDTO);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(UpdateAsync));

                return ResultFactory.Fail<TeacherDTO>(ErrorType.InternalError, DbErrorMessage);
            }
        }
    }
}
