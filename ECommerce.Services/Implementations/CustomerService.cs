using ECommerce.DataAccess.IRepositories;
using ECommerce.DTO.Request;
using ECommerce.DTO.Response;
using ECommerce.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Entities;

namespace ECommerce.Services.Implementations
{
    public class CustomerService : ICustomerServices
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CustomerDTOCollectionResponse> ListAsync(string filter, int page, int rows)
        {
            var response = new CustomerDTOCollectionResponse();
            try
            {
                var tuple = await _repository.ListAsync(filter ?? string.Empty, page, rows);

                response.Colletion = tuple.collection
                    .Select(c => new CustomerDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        LastName = c.LastName,
                        BirthDate = c.BirthDate,
                        Dni = c.Dni,
                        Email = c.Email
                    })
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                response.Success = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Task<BaseResponse<CustomerDTO>> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<string>> CreateAsync(CustomerRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                response.Result = await _repository.CreateAsync(new Customer
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    BirthDate = request.BirthDate,
                    Dni = request.Dni
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

        public Task<BaseResponse<string>> UpdateAsync(string id, CustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<string>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
