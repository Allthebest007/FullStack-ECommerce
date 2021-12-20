using InveonECommerce.Business.Engines.DTOs.Category;
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
    public class CategoryNotFoundFilter : ActionFilterAttribute
    {
        public ICategoryService _categoryService { get; set; }

        public CategoryNotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments["id"];
            Response<CategoryDTO> response = await _categoryService.GetByIdAsync(id);

            if (response.IsSuccessfull)
            {
                await next();
            }
            else
            {
                Response<CategoryDTO> errorResponse = Response<CategoryDTO>.Fail(new ErrorDTO($"{id} id'li item bulunamadı", false), 404);
                context.Result = new NotFoundObjectResult(errorResponse);
            }
        }
    }
}
