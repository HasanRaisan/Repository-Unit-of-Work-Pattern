using Application.Results;

namespace Application.Services.Generic
{
    public interface IGenericService<TReadDTO, TCreateDTO, TUpdateDTO> 
        where TReadDTO: class
        where TCreateDTO: class
        where TUpdateDTO  : class
    {
        Task<Result<TReadDTO>> GetByIDAsync(int id); 
        Task<Result<IEnumerable<TReadDTO>>> GetAllAsync();
        Task<Result<TReadDTO>> AddAsync(TCreateDTO DTO);
        Task<Result<TReadDTO>> UpdateAsync(TUpdateDTO DTO);
        Task<Result<bool>> DeleteAsync(int ID);
    }
}
