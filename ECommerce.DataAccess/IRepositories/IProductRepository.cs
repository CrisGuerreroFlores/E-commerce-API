using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;

namespace ECommerce.DataAccess.IRepositories
{
    public interface IProductRepository
    {
        Task<(ICollection<ProductInfo> collection, int total)> ListAsync(string filter, int page, int rows);
        Task<Product> GetItemAsync(string id);
        Task<string> CreateAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(string id);
    }
}
