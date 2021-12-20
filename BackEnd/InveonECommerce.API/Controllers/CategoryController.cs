using InveonECommerce.API.Filters;
using InveonECommerce.Business.Engines.DTOs.Category;
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
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {

            Response<List<CategoryDTO>> result = await _categoryService.GetAllAsync();
            return ActionResultInstance(result);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            
                Response<CategoryDTO> result = await _categoryService.GetCategoryWithProducts(id);
                return ActionResultInstance(result);
            
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDTO categoryDTO)
        {
            
                var result = await _categoryService.AddAsync(categoryDTO);
                return ActionResultInstance(result);
            
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDTO categoryDTO)
        {
            
                var result = _categoryService.Update(categoryDTO);
                return ActionResultInstance(result);

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var result = _categoryService.Delete(id);
            return ActionResultInstance(result);
        }
    }
}
