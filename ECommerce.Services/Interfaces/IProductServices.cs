using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.DTO.Request;
using ECommerce.DTO.Response;
using EcommerceAPI.DTO.Request;
using EcommerceAPI.DTO.Response;

namespace ECommerce.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ProductDTOColletionResponse> ListAsync(string filter, int page, int rows);
        Task<BaseResponse<ProductDTO>> GetAsync(string id);
        Task<BaseResponse<string>> CreateAsync(ProductRequest request);
        Task<BaseResponse<string>> UpdateAsync(string id, ProductRequest request);
        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
