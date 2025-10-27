using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Generic
{
    public interface IGenericService<TDTO> where TDTO : class
    {
        Task<TDTO?> GetByIDAsync(int id); 
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task AddAsync(TDTO DTO);
        void Update(TDTO DTO);
        void Delete(TDTO DTO);
    }
}
