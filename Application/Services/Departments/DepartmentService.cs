using Application.DTOs.Department;
using Application.DTOs.Teaher;
using Application.Results;
using Application.Services.Logging;
using AutoMapper;
using Domain.Entities.Department;
using FluentValidation;
using Infrastructure.Data.Entities;
using Infrastructure.UnitOfWork;
using System.Reflection;

namespace Application.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DepartmentCreateDTO> _createValidator;
        private readonly IValidator<DepartmentUpdateDTO> _updateValidator;
        private readonly IErrorLogService _logger;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<DepartmentCreateDTO> createValidator,
            IValidator<DepartmentUpdateDTO> updateValidator,
            IErrorLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            this._logger = logger;
        }
        private const string DbErrorMessage = "An unexpected database error occurred.";
        private const string PathForErrorLog = "TeacherSerivce";
        private Result<IEnumerable<DepartmentDTO>> EmptyList() => ResultFactory.Success(Enumerable.Empty<DepartmentDTO>());


        // ================================
        // Add Department
        // ================================
        public async Task<Result<DepartmentDTO>> AddAsync(DepartmentCreateDTO DTO)
        {
            // 1. Validate DTO
            var validationResult = await _createValidator.ValidateAsync(DTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<DepartmentDTO>(ErrorType.ValidationError, errors);
            }

            

            // 3. Domain business rules
            var domainCheck = DepartmentDomain.Create(DTO.Name,DTO.Description);
            if (!domainCheck.IsSuccess)
            {
                return ResultFactory.Fail<DepartmentDTO>(ErrorType.ValidationError, domainCheck.Errors);
            }
            //. Map DTO → Domain
            var domainModel = domainCheck.Data;

            // 4. Domain → Entity
            var entity = _mapper.Map<DepartmentEntity>(domainModel);

            try
            {
                await _unitOfWork.Departments.AddAsync(entity);

                // 5. Save
                var rows = await _unitOfWork.SaveChangesAsync();
                if (rows == 0)
                {
                    return ResultFactory.Fail<DepartmentDTO>(ErrorType.Conflict, DbErrorMessage);
                }

                // 6. Map back to DTO
                var saved = _mapper.Map<DepartmentDTO>(entity);
                return ResultFactory.Success(saved);
            }
            catch (Exception ex) 
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: MethodBase.GetCurrentMethod()?.Name ?? nameof(AddAsync));  // what r the diffrences (nameof(AddAsync), GetCurrentMethod)??

                return ResultFactory.Fail<DepartmentDTO>(ErrorType.InternalError, DbErrorMessage);
            }
        }

        // ================================
        // Delete Department
        // ================================
        public async Task<Result<bool>> DeleteAsync(int ID)
        {
            if (ID <= 0)
                return ResultFactory.Fail<bool>(ErrorType.ValidationError, "Invalid ID value.");

            var existing = await _unitOfWork.Departments.GetByIdAsync(ID);
            if (existing == null)
            {
                return ResultFactory.Fail<bool>(ErrorType.NotFound, $"Department with ID {ID} not found.");
            }

            try
            {
                _unitOfWork.Departments.Delete(existing);
                var rows = await _unitOfWork.SaveChangesAsync();

                if (rows > 0)
                    return ResultFactory.Success(true);

                return ResultFactory.Fail<bool>(ErrorType.Conflict, "Deletion failed. No rows affected.");
            }
            catch(Exception ex) 
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(DeleteAsync));

                return ResultFactory.Fail<bool>(ErrorType.InternalError, DbErrorMessage);
            }
        }

        // ================================
        // Get All Departments
        // ================================
        public async Task<Result<IEnumerable<DepartmentDTO>>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Departments.GetAllAsync();

                if (entities == null || !entities.Any())
                    return EmptyList();

                var dtos = _mapper.Map<IEnumerable<DepartmentDTO>>(entities);

                return ResultFactory.Success(dtos);
            }
            catch(Exception ex)
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(GetAllAsync));

                return ResultFactory.Fail<IEnumerable<DepartmentDTO>>(ErrorType.InternalError, DbErrorMessage);
            }
        }

        // ================================
        // Get Department by ID
        // ================================
        public async Task<Result<DepartmentDTO>> GetByIDAsync(int id)
        {
            if (id <= 0)
                return ResultFactory.Fail<DepartmentDTO>(ErrorType.ValidationError, "Invalid ID value.");

            try
            {
                var entity = await _unitOfWork.Departments.GetByIdAsync(id);

                if (entity == null)
                    return ResultFactory.Fail<DepartmentDTO>(ErrorType.NotFound, $"Department with ID {id} not found.");

                var dto = _mapper.Map<DepartmentDTO>(entity);
                return ResultFactory.Success(dto);
            }
            catch (Exception ex) 
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(GetByIDAsync));

                return ResultFactory.Fail<DepartmentDTO>(ErrorType.InternalError, DbErrorMessage);
            }
        }

        // ================================
        // Update Department
        // ================================
        public async Task<Result<DepartmentDTO>> UpdateAsync(DepartmentUpdateDTO DTO)
        {
            // 1. Find existing entity
            var existing = await _unitOfWork.Departments.GetByIdAsync(DTO.Id);
            if (existing == null)
                return ResultFactory.Fail<DepartmentDTO>(ErrorType.NotFound, "Department not found.");
            

            // 2. Validate DTO
            var validationResult = await _updateValidator.ValidateAsync(DTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<DepartmentDTO>(ErrorType.ValidationError, errors);
            }



            // 4. Domain business rules
            var domainCheck = DepartmentDomain.Update(DTO.Id, DTO.Name, DTO.Description);
            if (!domainCheck.IsSuccess)
                return ResultFactory.Fail<DepartmentDTO>(ErrorType.ValidationError, domainCheck.Errors);

            //DTO → Domain
            var domainModel = domainCheck.Data;

            try
            {
                // 5. Update existing entity (tracking-safe)
                _mapper.Map(domainModel, existing);
                _unitOfWork.Departments.Update(existing);

                // 6. Save
                var rows = await _unitOfWork.SaveChangesAsync();
                if (rows == 0)
                {
                    return ResultFactory.Fail<DepartmentDTO>(ErrorType.Conflict, "Update failed. No changes detected.");
                }

                // 7. Reload updated
                var updated = await _unitOfWork.Departments.GetByIdAsync(DTO.Id);
                var updatedDTO = _mapper.Map<DepartmentDTO>(updated);

                return ResultFactory.Success(updatedDTO);
            }
            catch(Exception ex) 
            {
                await _logger.LogAsync(Ex: ex, Path: PathForErrorLog, Method: nameof(UpdateAsync));

                return ResultFactory.Fail<DepartmentDTO>(ErrorType.InternalError, DbErrorMessage);
            }
        }

    }
}
