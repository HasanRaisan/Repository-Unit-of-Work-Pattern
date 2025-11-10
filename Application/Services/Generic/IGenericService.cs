using Application.Results;


namespace Application.Services.Generic
{
    public interface IGenericService<DTO> where DTO : class
    {
        Task<Result<DTO>> GetByIDAsync(int id); 
        Task<Result<IEnumerable<DTO>>> GetAllAsync();
        Task<Result<DTO>> AddAsync(DTO DTO);
        Task<Result<DTO>> UpdateAsync(DTO DTO);
        Task<Result<bool>> DeleteAsync(int ID);
    }
}
