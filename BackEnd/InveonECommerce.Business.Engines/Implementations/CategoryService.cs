using AutoMapper;
using InveonECommerce.Business.Engines.Constants.Messages;
using InveonECommerce.Business.Engines.DTOs.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uof;
        private readonly IRepository<CategoryEntity> _categoryRepo;
        private IMapper _mapper;

        public CategoryService(IUnitOfWork uof,IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
            _categoryRepo = _uof.GetRepository<CategoryEntity>();
        }

        public async Task<Response<AddCategoryDTO>> AddAsync(AddCategoryDTO categoryDTO)
        {
            await _categoryRepo.AddAsync(_mapper.Map<CategoryEntity>(categoryDTO));
            int result = await _uof.CommitAsync();

            if(result > 0)
            {
                return Response<AddCategoryDTO>.Success(categoryDTO, 200);
            }
            else
            {
                return Response<AddCategoryDTO>.Fail(CategoryMessages.FailAdded,400,false);
            }

            
        }

        public Response<NoDataDTO> Delete(int id)
        {
            CategoryEntity existCategory = CheckIfCategoryExist(id);

            if(existCategory != null)
            {
                // If category has products, then set "IsDeleted" property of product to true.
                SetDeleteProductIfExist(existCategory);

                existCategory.IsDeleted = true;

                _categoryRepo.Update(existCategory);
                int result = _uof.Commit();

                if(result > 0)
                {
                    return Response<NoDataDTO>.Success(204);
                }
                else
                {
                    return Response<NoDataDTO>.Fail(CategoryMessages.FailDeleted, 400, false);
                }
            }

            return Response<NoDataDTO>.Fail(CategoryMessages.NotFound, 400, false);

        }

        public async Task<Response<List<CategoryDTO>>> GetAllAsync()
        {
            return Response<List<CategoryDTO>>.Success(
                _mapper.Map<List<CategoryDTO>>
                (await _categoryRepo.GetAllAsync(x=> x.IsDeleted == false)), 200);
        }

        public async Task<Response<CategoryDTO>> GetByIdAsync(int id)
        {
            CategoryEntity existCategory = await CheckIfCategoryExistAsync(id);
            if (existCategory != null)
            {
                return Response<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(existCategory), 200);
            }
            else
            {
                return Response<CategoryDTO>.Fail(CategoryMessages.NotFound,400,false);
            }
        }

        public Response<NoDataDTO> Update(UpdateCategoryDTO categoryDTO)
        {
            CategoryEntity existCategory = CheckIfCategoryExist(categoryDTO.Id);
            if(existCategory != null)
            {
                existCategory.Name = categoryDTO.Name;
                _categoryRepo.Update(existCategory);
                _uof.Commit();
                return Response<NoDataDTO>.Success(200);

            }
            else
            {
                return Response<NoDataDTO>.Fail(CategoryMessages.FailUpdated, 400,false);
            }
        }

        public async Task<Response<CategoryDTO>> GetCategoryWithProducts(int id)
        {
            CategoryEntity existCategory = _categoryRepo.GetByIdAsync(id).Result;
            if (existCategory == null)
            {
                return Response<CategoryDTO>.Fail(CategoryMessages.NotFound, 404, false);
            }
            
            existCategory = await _categoryRepo.Get(x => x.Id == id).Include(x => x.Products).FirstOrDefaultAsync();
            return Response<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(existCategory), 200);
        }

        private void SetDeleteProductIfExist(CategoryEntity category)
        {
            if (category.Products.Count > 0)
            {
                foreach (ProductEntity product in category.Products)
                {
                    product.IsDeleted = true;
                }
            }
        }

        private async Task<CategoryEntity> CheckIfCategoryExistAsync(int categoryid)
        {
            CategoryEntity existCategory = await _categoryRepo.GetByIdAsync(categoryid);
            if (existCategory != null)
            {
                return existCategory;
            }
            else
            {
                return null;
            }
        }

        private CategoryEntity CheckIfCategoryExist(int categoryid)
        {
            CategoryEntity existCategory = _categoryRepo.GetById(categoryid);
            if (existCategory != null)
            {
                return existCategory;
            }
            else
            {
                return null;
            }
        }
    }
}
