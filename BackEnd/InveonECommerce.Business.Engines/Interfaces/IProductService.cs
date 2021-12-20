using InveonECommerce.Business.Engines.DTOs.Product;
using InveonECommerce.Business.Engines.DTOs.RequestDTO;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Interfaces
{
    public interface IProductService : IServiceBase 
    {
        Task<Response<ProductDTO>> AddAsync(AddProductDTO productDTO);
        Response<NoDataDTO> Update(UpdateProductDTO productDTO);
        Response<NoDataDTO> Delete(int id);
        Task<Response<ProductDTO>> GetByIdAsync(int id);

        Task<Response<List<ProductDTO>>> GetProductsByPriceFilter(ProductPriceRangeDTO productPriceRangeDTO);
        Task<Response<List<ProductDTO>>> GetProductsByNameFilter(string productName);
        Task<Response<List<ProductDTO>>> GetAllAsync();

        Task<Response<List<ProductDTO>>> GetProductListWithPagination(ProductListRequestDTO productListRequestDto);

    }
}
