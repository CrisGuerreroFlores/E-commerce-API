using ECommerce.DataAccess.IRepositories;
using ECommerce.DTO.Request;
using ECommerce.DTO.Response;
using ECommerce.Services.Interfaces;
using ECommerceAPI.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Implementations
{
    public class ProductService : IProductServices
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProductDTOColletionResponse> ListAsync(string filter, int page, int rows)
        {
            var response = new ProductDTOColletionResponse();
            try
            {
                var result = await _repository.ListAsync(filter ?? string.Empty, page, rows);

                response.Colletion = result.collection
                    .Select(c => new ProductDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Url = c.ProductUrl,
                        UnitPrice = c.UnitPrice,
                        CategoryId = c.Category
                    })
                    .ToList();

                var totalPages = result.total / rows;
                if ((result.total % rows) > 0)
                {
                    totalPages++;
                }

                response.TotalPages = totalPages;
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

        public async Task<BaseResponse<ProductDTO>> GetAsync(string id)
        {
            var response = new BaseResponse<ProductDTO>();
            try
            {
                var product = await _repository.GetItemAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Register no found";
                    return response;
                }

                response.Result = new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Url = product.ProductUrl,
                    Active = product.Active,
                    UnitPrice = product.UnitPrice
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

        public async Task<BaseResponse<string>> CreateAsync(ProductRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                response.Result = await _repository.CreateAsync(new Product()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Active = request.Active,
                    CategoryId = request.CategoryId,
                    UnitPrice = request.UnitPrice,
                    ProductUrl = request.FileName
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

        public async Task<BaseResponse<string>> UpdateAsync(string id, ProductRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                await _repository.UpdateAsync(new Product()
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
