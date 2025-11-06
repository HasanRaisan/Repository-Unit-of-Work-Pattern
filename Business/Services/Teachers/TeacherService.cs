using AutoMapper;
using Business.Domains.Core;
using Business.DTOs.Student;
using Business.Result;
using Clean_Three_Tier_First.DTOs.Teaher;
using Data.Data.Entities;
using Data.UnitOfWork;
using FluentValidation;

namespace Business.Services.Teachers
{
    public class TeacherService : ITeacher
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<TeacherDomain> _validator;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TeacherDomain> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<Result<TeacherDTO>> AddAsync(TeacherDTO DTO)
        {
            var teacherDomain = _mapper.Map<TeacherDomain>(DTO);

            // validation
            var validationResult = await _validator.ValidateAsync(teacherDomain);

            if (!validationResult.IsValid)
            {
                return new Result<TeacherDTO>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            // add
            var teacherEntity = _mapper.Map<TeacherEntity>(teacherDomain);
            try
            {
                await _unitOfWork.Teachers.AddAsync(teacherEntity);
                await _unitOfWork.SaveChangesAsync(); // don't we need to check affected number ?? not just in update and delete (soft failures)

                var savedDTO = _mapper.Map<TeacherDTO>(teacherEntity);
                return new Result<TeacherDTO>()
                {
                    IsSuccess = true,
                    Data = savedDTO
                };
            }
            catch (Exception ex)
            {
                return new Result<TeacherDTO>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "An unexpected database error occurred: " + ex.Message }
                };
            }
        }

        public async Task<Result<bool>> DeleteAsync(int ID)
        {
            var teacherToDelete = await _unitOfWork.Teachers.GetByIdAsync(ID);

            if (teacherToDelete == null)
            {
                return new Result<bool>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Teacher with ID {ID} not found." }
                };
            }

            try
            {
                _unitOfWork.Teachers.Delete(teacherToDelete);

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

        public async Task<Result<IEnumerable<TeacherDTO>>> GetAllAsync()
        {
            try
            {
                var teacherEntities = await _unitOfWork.Teachers.GetAllAsync();
                if (teacherEntities == null || !teacherEntities.Any())
                {
                    return new Result<IEnumerable<TeacherDTO>>
                    {
                        IsSuccess = true,
                        Data = Enumerable.Empty<TeacherDTO>()
                    };
                }

                var teacherDTOs = _mapper.Map<IEnumerable<TeacherDTO>>(teacherEntities);
                return new Result<IEnumerable<TeacherDTO>>
                {
                    IsSuccess = true,
                    Data = teacherDTOs
                };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<TeacherDTO>>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"An error occurred while retrieving teachers: {ex.Message}" }
                };
            }
        }

        public async Task<Result<TeacherDTO>> GetByIDAsync(int id)
        {
            try
            {
                var teacherEntity = await _unitOfWork.Teachers.GetByIdAsync(id);

                if (teacherEntity == null)
                {
                    return new Result<TeacherDTO>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"Teacher with ID {id} not found." }
                    };
                }

                var teacherDTO = _mapper.Map<TeacherDTO>(teacherEntity);

                return new Result<TeacherDTO>
                {
                    IsSuccess = true,
                    Data = teacherDTO
                };
            }
            catch (Exception ex)
            {
                return new Result<TeacherDTO>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"An error occurred while retrieving teacher: {ex.Message}" }
                };
            }
        }

        public async Task<Result<IEnumerable<TeacherDTO>>> GetTeachersByDepartmentAsync(int Id)
        {
            try
            {
                var teacherEntities = await _unitOfWork.Teachers.GetTeachersByDepartmentAsync(Id);
                if (teacherEntities == null || !teacherEntities.Any())
                {
                    return new Result<IEnumerable<TeacherDTO>>
                    {
                        IsSuccess = true,
                        Data = Enumerable.Empty<TeacherDTO>()
                    };
                }

                var teacherDTOs = _mapper.Map<IEnumerable<TeacherDTO>>(teacherEntities);
                return new Result<IEnumerable<TeacherDTO>>
                {
                    IsSuccess = true,
                    Data = teacherDTOs
                };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<TeacherDTO>>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"An error occurred while retrieving teacher: {ex.Message}" }
                };
            }
        }

        public async Task<Result<TeacherDTO>> UpdateAsync(TeacherDTO DTO)
        {
            var teacherDomain = _mapper.Map<TeacherDomain>(DTO);

            var validationResult = await _validator.ValidateAsync(teacherDomain);

            if (!validationResult.IsValid)
            {
                return new Result<TeacherDTO>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            try
            {
                var teacherEntity = _mapper.Map<TeacherEntity>(teacherDomain);
                _unitOfWork.Teachers.Update(teacherEntity);

                int affectedRows = await _unitOfWork.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    return new Result<TeacherDTO>
                    {
                        IsSuccess = true,
                        Data = DTO
                    };
                }
                else
                {
                    return new Result<TeacherDTO>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Update failed. No corresponding teacher found or no changes detected." }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<TeacherDTO>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Database error during update: {ex.Message}" }
                };
            }
        }
    }
}
