using ECommerce.Services.Interfaces;
using EcommerceAPI.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ECommerce.DTO.Response;
using EcommerceAPI.DTO.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _service;

        public CategoriesController(ICategoryServices service)
        {
            _service = service;
        }
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(CategoryDTOCollectionResponse))]
        public async Task<IActionResult> GetCategories(string filter, int page = 1, int rows = 10)
        {
            #region Depacrated

            //var response = new CategoryDTOCollectionResponse();
            //try
            //{
            //    response.Colletion = _context.Categories.ToList()
            //        .Select(c => new CategoryDTO()
            //        {
            //            Id = c.Id,
            //            Name = c.Name,
            //            Description = c.Description
            //        }).ToList();
            //}
            //catch (Exception e)
            //{
            //    response.ErrorMessage = e.Message;
            //}

            //return Ok(response);

            #endregion
            return Ok(await _service.ListAsync(filter, page, rows));
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<CategoryDTO>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseResponse<CategoryDTO>))]
        public async Task<IActionResult> GetCategories(string id)
        {
            #region Depacrated

            //var response = new BaseResponse<CategoryDTO>();

            //var find = _context.Categories
            //    .FirstOrDefault(c => c.Id == id && c.Status);

            //if (find == null)
            //{
            //    return NotFound(response);
            //}
            //response.Success = true;
            //response.Result = new CategoryDTO()
            //{
            //    Id = find.Id,
            //    Name = find.Name,
            //    Description = find.Description
            //};
            //return Ok(response);

            #endregion
            var response = await _service.GetAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPost]
        [SwaggerResponse(201, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PostCategories([FromBody] CategoryRequest request)
        {
            #region Depacrated

            //var response = new BaseResponse<string>();

            //var category = new Category()
            //{
            //    // Id = Guid.NewGuid().ToString(),
            //    Name = request.Name,
            //    Description = request.Description
            //};
            //_context.Categories.Add(category);
            //_context.SaveChanges();

            //response.Success = true;
            //response.Result = category.Id;

            //return CreatedAtAction("GetCategories", new
            //{
            //    id = response.Result
            //}, response);

            #endregion
            var response = await _service.CreateAsync(request);

            return CreatedAtAction("GetCategories", new
            {
                id = response.Result
            }, response);
        }
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PutCategories(string id, [FromBody] CategoryRequest request)
        {
            #region Depacrated

            //var response = new BaseResponse<string>();
            //var find = this._context.Categories.FirstOrDefault(c => c.Id == id);

            //if (find == null)
            //{
            //    return NotFound(response);
            //}

            //find.Description = request.Description;
            //find.Name = request.Name;

            //_context.Categories.Attach(find);
            //_context.Entry(find).State = EntityState.Modified;
            //_context.SaveChanges();

            //response.Success = true;
            //response.Result = id;
            //return Ok(response);

            #endregion

            return Ok(await _service.UpdateAsync(id, request));
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> DeleteCategories(string id)
        {
            #region Depacrated

            //var response = new BaseResponse<string>();
            //var find = this._context.Categories.FirstOrDefault(c =>
            //    c.Id == id);
            //if (find == null)
            //{
            //    return NotFound(response);
            //}

            //find.Status = false;
            //_context.Categories.Attach(find);
            //_context.Entry(find).State = EntityState.Modified;
            //_context.SaveChanges();

            //response.Success = true;
            //response.Result = id;
            //return Ok(response);

            #endregion

            return Ok(await _service.DeleteAsync(id));
        }
    }
}
