using InveonECommerce.Business.Engines.DTOs.Product;
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
    public class ProductNotFoundFilter : ActionFilterAttribute
    {
        public IProductService _productService{ get; set; }

        public ProductNotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments["id"];
            Response<ProductDTO> response = await _productService.GetByIdAsync(id);

            if (response.IsSuccessfull)
            {
                await next();
            }
            else
            {
                Response<ProductDTO> errorResponse = Response<ProductDTO>.Fail(new ErrorDTO($"{id} id'li item bulunamadı", false),404);
                context.Result = new NotFoundObjectResult(errorResponse);
            }
        }
    }
}
