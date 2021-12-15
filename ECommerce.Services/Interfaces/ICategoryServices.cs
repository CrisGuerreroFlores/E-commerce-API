using EcommerceAPI.DTO.Response;
using System.Threading.Tasks;
using ECommerce.DTO.Response;
using EcommerceAPI.DTO.Request;

namespace ECommerce.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<CategoryDTOCollectionResponse> ListAsync(string filter, int page, int rows);
        Task<BaseResponse<CategoryDTO>> GetAsync(string id);
        Task<BaseResponse<string>> CreateAsync(CategoryRequest request);
        Task<BaseResponse<string>> UpdateAsync(string id, CategoryRequest request);
        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
