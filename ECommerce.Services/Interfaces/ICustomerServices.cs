using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.DTO.Request;
using ECommerce.DTO.Response;

namespace ECommerce.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<CustomerDTOCollectionResponse> ListAsync(string filter, int page, int rows);
        Task<BaseResponse<CustomerDTO>> GetAsync(string id);
        Task<BaseResponse<string>> CreateAsync(CustomerRequest request);
        Task<BaseResponse<string>> UpdateAsync(string id, CustomerRequest request);
        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
