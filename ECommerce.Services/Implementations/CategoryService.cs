using ECommerce.DataAccess.IRepositories;
using ECommerce.DTO.Response;
using ECommerce.Services.Interfaces;
using EcommerceAPI.DTO.Request;
using EcommerceAPI.DTO.Response;
using EcommerceAPI.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Implementations
{
    public class CategoryService : ICategoryServices
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ILogger<CategoryService> logger, ICategoryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<CategoryDTOCollectionResponse> ListAsync(string filter, int page, int rows)
        {
            var response = new CategoryDTOCollectionResponse();
            try
            {
                var result = await _repository.ListAsync(filter ?? string.Empty, page, rows);

                response.Colletion = result.collection
                    .Select(c => new CategoryDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    })
                    .ToList();

                response.TotalPages = result.total / rows;
                if ((result.total % rows) > 0)
                {
                    response.TotalPages++;
                }

                response.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                response.Success = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public async Task<BaseResponse<CategoryDTO>> GetAsync(string id)
        {
            var response = new BaseResponse<CategoryDTO>();
            try
            {
                var category = await _repository.GetItemAsync(id);
                if (category == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Register no found";
                    return response;
                }

                response.Result = new CategoryDTO()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };

                response.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                response.Success = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> CreateAsync(CategoryRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                response.Result = await _repository.CreateAsync(new Category()
                {
                    Name = request.Name,
                    Description = request.Description
                });

                response.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                response.Success = false;
                response.ErrorMessage = e.Message;
            }
            return response;
        }

        public async Task<BaseResponse<string>> UpdateAsync(string id, CategoryRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                await _repository.UpdateAsync(new Category()
                {
                    Id = id,
                    Name = request.Name,
                    Description = request.Description
                });

                response.Result = id;
                response.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                response.Success = false;
                response.ErrorMessage = e.Message;
            }
            return response;
        }

        public async Task<BaseResponse<string>> DeleteAsync(string id)
        {
            var response = new BaseResponse<string>();
            try
            {
                await _repository.DeleteAsync(id);
                response.Result = id;
                response.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                response.Success = false;
                response.ErrorMessage = e.Message;
            }
            return response;
        }
    }
}
