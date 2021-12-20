using AutoMapper;
using InveonECommerce.Business.Engines.Constants.Messages;
using InveonECommerce.Business.Engines.DTOs.Product;
using InveonECommerce.Business.Engines.DTOs.RequestDTO;
using InveonECommerce.Business.Engines.Interfaces;
using InveonECommerce.Data.DAL.Repository;
using InveonECommerce.Data.DAL.UnitOfWork;
using InveonECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Implementations
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        IRepository<ProductEntity> _productRepo = null;
        IRepository<CategoryEntity> _categoryRepo = null;

        public ProductService(IUnitOfWork uof,IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
            _productRepo = _uof.GetRepository<ProductEntity>();
            _categoryRepo = _uof.GetRepository<CategoryEntity>();
        }

        public async Task<Response<ProductDTO>> AddAsync(AddProductDTO productDTO)
        {
            CategoryEntity existCategory = _categoryRepo.GetById(productDTO.CategoryId);

            if(existCategory == null)
            {
                return Response<ProductDTO>.Fail(CategoryMessages.NotFound, 400, false);
            }

            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            productEntity.CategoryId = existCategory.Id;
            productEntity.IsDeleted = false;

            await _productRepo.AddAsync(productEntity);
            int result = await _uof.CommitAsync();
            
            if(result > 0)
            {
                ProductDTO productDto = _mapper.Map<ProductDTO>(productEntity);
                return Response<ProductDTO>.Success(productDto, 200);
            }
            else
            {
                return Response<ProductDTO>.Fail(ProductMessages.FailAdded, 400, false);
            }

           
            
        }

        


        public Response<NoDataDTO> Delete(int id)
        {
            ProductEntity existProduct = _productRepo.GetById(id);
            
            existProduct.IsDeleted = true;

            SetDeleteProductImageIfExist(existProduct);


            _productRepo.Update(existProduct);
            int result = _uof.Commit();

            if (result > 0)
            {
                return Response<NoDataDTO>.Success(204);
            }
            else
            {
                return Response<NoDataDTO>.Fail(ProductMessages.FailDeleted, 400, false);
            }
        }

        public async Task<Response<List<ProductDTO>>> GetAllAsync()
        {
            List<ProductDTO> productListDto = _mapper.Map<List<ProductDTO>>(await _productRepo.GetAllAsync(x => x.IsDeleted == false));
            return Response<List<ProductDTO>>.Success(productListDto, 200);
        }

        public async Task<Response<List<ProductDTO>>> GetProductListWithPagination(ProductListRequestDTO productListRequestDto)
        {
            IQueryable<ProductEntity> allProducts = _productRepo.Get(x=> 1==1);
            if (productListRequestDto.CategoryId > 0)
            {
                allProducts = allProducts.Where(q => q.CategoryId == productListRequestDto.CategoryId);
            }
            if (productListRequestDto.OrderBy != null)
            {
                switch (productListRequestDto.OrderBy)
                {
                    case OrderBy.PriceAsc:
                        allProducts = allProducts.OrderBy(q => q.Price);
                        break;
                    case OrderBy.PriceDesc:
                        allProducts = allProducts.OrderByDescending(q => q.Price);
                        break;
                }
            }
            List<ProductEntity> products = await allProducts.Skip((productListRequestDto.PageIndex - 1) * productListRequestDto.PageSize).Take(productListRequestDto.PageSize).ToListAsync();
            List<ProductDTO> productListDto = _mapper.Map<List<ProductDTO>>(products);
             

            return Response<List<ProductDTO>>.Success(productListDto, 200);
        }

        public async Task<Response<List<ProductDTO>>> GetProductsByPriceFilter(ProductPriceRangeDTO productPriceRangeDTO)
        {
            List<ProductEntity> filteredProducts = await _productRepo.Get(x => x.Price >= productPriceRangeDTO.minValue && x.Price <= productPriceRangeDTO.maxValue).ToListAsync();
            return Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(filteredProducts), 200);
        }

       

        public async Task<Response<List<ProductDTO>>> GetProductsByNameFilter(string productName)
        {
            List<ProductEntity> filteredProducts = await _productRepo.Get(x => x.Name.Contains(productName)).ToListAsync();
            return Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(filteredProducts), 200);
        }

        

        public async Task<Response<ProductDTO>> GetByIdAsync(int id)
        {

            ProductEntity existProduct = await _productRepo.GetByIdAsync(id);

            if(existProduct != null)
            {
                ProductDTO productDto = _mapper.Map<ProductDTO>(existProduct);
                return Response<ProductDTO>.Success(productDto, 200);
            }
            else
            {
                return Response<ProductDTO>.Fail(ProductMessages.NotFound,400,false);
            }
        }

        public Response<NoDataDTO> Update(UpdateProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            _productRepo.Update(productEntity);
            int result = _uof.Commit();

            if (result > 0)
            {
                
                return Response<NoDataDTO>.Success(204);
            }
            else
            {
                return Response<NoDataDTO>.Fail(ProductMessages.FailUpdated,400,false);
            }
        }

        private void SetDeleteProductImageIfExist(ProductEntity product)
        {
            if(product.ProductImage != null)
            {
                product.ProductImage.IsDeleted = true;
            }
        }

        
    }
}
