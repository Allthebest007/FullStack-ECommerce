using InveonECommerce.API.Filters;
using InveonECommerce.Business.Engines.DTOs.AddressInfo;
using InveonECommerce.Business.Engines.DTOs.Order;
using InveonECommerce.Business.Engines.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDTO orderDTO)
        {
            
            var result = await _orderService.AddOrderAsync(orderDTO,HttpContext.User.Identity.Name);
            return ActionResultInstance(result);
           

        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            Response<OrderDetailDTO> result = await _orderService.GetOrderDetails(id);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            Response<List<ListOrderDTO>> orderList = await _orderService.GetAllAsync();
            return ActionResultInstance(orderList);
        }
    }
}
