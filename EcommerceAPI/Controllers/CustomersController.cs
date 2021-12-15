using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.DTO.Request;
using ECommerce.DTO.Response;
using ECommerce.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _services;

        public CustomersController(ICustomerServices services)
        {
            _services = services;
        }
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(CustomerDTOCollectionResponse))]
        public async Task<IActionResult> GetCustomers(string filter, int page = 1, int rows = 10)
        {
            return Ok(await _services.ListAsync(filter, page, rows));
        }
        [HttpPost]
        [SwaggerResponse(201, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerRequest request)
        {
            var response = await _services.CreateAsync(request);

            return CreatedAtAction("GetCustomers", new
            {
                id = response.Result
            }, response);
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<CustomerDTO>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseResponse<CustomerDTO>))]
        public async Task<IActionResult> GetCategories(string id)
        {
            var response = await _services.GetAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PutCategories(string id, [FromBody] CustomerRequest request)
        {
            return Ok(await _services.UpdateAsync(id, request));
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> DeleteCustomers(string id)
        {
            return Ok(await _services.DeleteAsync(id));
        }
    }
}
