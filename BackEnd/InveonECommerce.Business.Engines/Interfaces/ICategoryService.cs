using InveonECommerce.Business.Engines.DTOs.Category;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Interfaces
{
    public interface ICategoryService : IServiceBase
    {
        Task<Response<AddCategoryDTO>> AddAsync(AddCategoryDTO categoryDTO);
        Response<NoDataDTO> Update(UpdateCategoryDTO categoryDTO);
        Response<NoDataDTO> Delete(int id);
        Task<Response<CategoryDTO>> GetByIdAsync(int id);

        Task<Response<List<CategoryDTO>>> GetAllAsync();
        Task<Response<CategoryDTO>> GetCategoryWithProducts(int id);
    }
}
