using InveonECommerce.Business.Engines.DTOs.Order;
using InveonECommerce.Business.Engines.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.API.Filters
{
    public class OrderNotFoundFilter : ActionFilterAttribute
    {
        public IOrderService _orderService { get; set; }

        public OrderNotFoundFilter(IOrderService orderService)
        {
            _orderService = orderService;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments["id"];
            Response<OrderDetailDTO> response = await _orderService.GetByIdAsync(id);

            if (response.IsSuccessfull)
            {
                await next();
            }
            else
            {
                Response<OrderDetailDTO> errorResponse = Response<OrderDetailDTO>.Fail(new ErrorDTO($"{id} id'li item bulunamadı", false), 404);
                context.Result = new NotFoundObjectResult(errorResponse);
            }
        }
    }
}
