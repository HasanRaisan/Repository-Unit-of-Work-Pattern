﻿using Business.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Generic
{
    public interface IGenericService<DTO> where DTO : class
    {
        Task<Result<DTO>> GetByIDAsync(int id); 
        Task<Result<IEnumerable<DTO>>> GetAllAsync();
        Task<Result<DTO>> AddAsync(DTO DTO);
        Result<DTO> Update(DTO DTO);
        Result<DTO> Delete(DTO DTO);
    }
}
