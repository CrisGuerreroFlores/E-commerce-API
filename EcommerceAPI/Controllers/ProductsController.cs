using ECommerce.DTO.Request;
using ECommerce.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //ProductDTOColletionResponse _context;
        private readonly IProductServices _service;
        public ProductsController(IProductServices service)
        {
            _service = service;
        }
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(ProductDTOColletionResponse))]
        public async Task<IActionResult> GetProducts(string filter, int page = 1, int rows = 10)
        {
            #region Depacrated

            //_context.Success = true;
            //return Ok(_context);

            #endregion

            return Ok(await _service.ListAsync(filter, page, rows));
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<ProductDTO>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseResponse<ProductDTO>))]
        public async Task<IActionResult> GetProducts(string id)
        {
            #region Depacrated

            //var response = new BaseResponse<ProductDTO>();

            //var find = _context.Colletion.FirstOrDefault(p => p.Id == id);

            //if (find == null)
            //{
            //    return NotFound(response);
            //}
            //response.Success = true;
            //response.Result = find;

            //return Ok(response);

            #endregion

            var response = await _service.GetAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PutProduct(string id, ProductRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            return Ok(response);
        }

        [SwaggerResponse(201, "Ok", typeof(BaseResponse<string>))]
        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] ProductRequest request)
        {
            #region Depacrated

            //var response = new BaseResponse<string>();

            //var product = new ProductDTO
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = request.Name,
            //    Description = request.Description,
            //    Url = request.FileName
            //};

            //_context.Colletion.Add(product);
            //response.Success = true;
            //response.Result = product.Id;

            //return CreatedAtAction("GetProducts", new
            //{
            //    Id = product.Id
            //}, response);

            #endregion
            var response = await _service.CreateAsync(request);

            return CreatedAtAction("GetProducts", new
            {
                id = response.Result
            }, response);
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> DeleteCategories(string id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
