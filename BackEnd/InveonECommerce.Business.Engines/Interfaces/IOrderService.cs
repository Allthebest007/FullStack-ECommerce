using InveonECommerce.Business.Engines.DTOs.Order;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Interfaces
{
    public interface IOrderService : IServiceBase
    {
        
        Task<Response<AddOrderDTO>> AddOrderAsync(AddOrderDTO orderDto,string username);
        Task<Response<OrderDetailDTO>> GetOrderDetails(int id);
        Task<Response<OrderDetailDTO>> GetByIdAsync(int id);

        Task<Response<List<ListOrderDTO>>> GetAllAsync();

       
    }
}
