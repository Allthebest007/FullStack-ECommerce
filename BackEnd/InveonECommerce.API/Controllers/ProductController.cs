using InveonECommerce.API.Filters;
using InveonECommerce.Business.Engines.DTOs.Product;
using InveonECommerce.Business.Engines.DTOs.RequestDTO;
using InveonECommerce.Business.Engines.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllAsync();
            return ActionResultInstance(result);
            
        }

       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            Response<ProductDTO> res = await _productService.GetByIdAsync(id);
            return ActionResultInstance(res);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductsByNameFilter(string name)
        {

            Response<List<ProductDTO>> res = await _productService.GetProductsByNameFilter(name);
            return ActionResultInstance(res);
        }

        
        [HttpGet("pricefilter")]
        public async Task<IActionResult> GetProductsByPriceFilter(ProductPriceRangeDTO productPriceRangeDto)
        {

            Response<List<ProductDTO>> res = await _productService.GetProductsByPriceFilter(productPriceRangeDto);
            return ActionResultInstance(res);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetProductsByPagination(ProductListRequestDTO productListRequestDto)
        {

            Response<List<ProductDTO>> res = await _productService.GetProductListWithPagination(productListRequestDto);
            return ActionResultInstance(res);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDTO productDTO)
        {
            var result = await _productService.AddAsync(productDTO);
            return ActionResultInstance(result);
            
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDTO productDTO)
        {
            var result = _productService.Update(productDTO);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.Delete(id);
            return ActionResultInstance(result);
        }

    }
}
