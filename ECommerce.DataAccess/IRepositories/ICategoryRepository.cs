using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceAPI.Entities;

namespace ECommerce.DataAccess.IRepositories
{
    public interface ICategoryRepository
    {
        Task<(ICollection<Category> collection, int total)> ListAsync(string filter, int page, int rows);
        Task<Category> GetItemAsync(string id);
        Task<string> CreateAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(string id);
    }
}
