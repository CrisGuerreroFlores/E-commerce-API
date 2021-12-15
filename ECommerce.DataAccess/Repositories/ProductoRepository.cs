using System;
using ECommerce.DataAccess.IRepositories;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceAPI.Entities.Complex;

namespace ECommerce.DataAccess.Repositories
{
    public class ProductoRepository : ECommerceContextBase<Product>, IProductRepository
    {
        public ProductoRepository(ECommerceDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<(ICollection<ProductInfo> collection, int total)> ListAsync(string filter, int page, int rows)
        {
            #region Depacrated

            //var collection = await _dbContext.Set<Product>()
            //    .Where(p => p.Name.StartsWith(filter) && p.Status)
            //    .OrderBy(c => c.Name)
            //    .AsNoTracking() //que cambios han ocurrido en los registros.
            //    .Select(p=> new ProductInfo
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description,
            //        Category = p.Category.Name,
            //        UnitPrice = p.UnitPrice,
            //        ProductUrl = p.ProductUrl
            //    })
            //    .Skip((page - 1) * rows)
            //    .Take(rows)
            //    .ToListAsync();

            //var totalCount = await _dbContext.Set<Product>()
            //    .Where(c => c.Name.StartsWith(filter) && c.Status)
            //    .AsNoTracking()
            //    .CountAsync();

            //return (collection.ToList(), totalCount);
            #endregion

            Expression<Func<Product, bool>> predicate = p => p.Name.Contains(filter)
                                                             && p.Status;
            Expression<Func<Product, ProductInfo>> selector = p => new ProductInfo
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category.Name,
                UnitPrice = p.UnitPrice,
                ProductUrl = p.ProductUrl
            };

            Expression<Func<Product, string>> orderBy = p => p.Name;
            return await ListCollection(selector, predicate, page, rows);

        }

        public async Task<Product> GetItemAsync(string id)
        {
            #region Depacrated

            //return await _dbContext.Set<Product>()
            //    .SingleOrDefaultAsync(c => c.Id == id);
            #endregion

            return await Context.Select<Product>(id);
        }

        public async Task<string> CreateAsync(Product entity)
        {
            #region Depacrated

            //await _dbContext.Set<Product>().AddAsync(entity);
            //_dbContext.Entry(entity).State = EntityState.Added;
            //await _dbContext.SaveChangesAsync();

            //return entity.Id;

            #endregion

            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            #region Depacrated

            ////_dbContext.Set<Product>().Attach(entity);
            //var register = await _dbContext.Set<Product>().FindAsync(entity.Id);

            //if (register == null) return;

            //register.Description = entity.Description;
            //register.Name = entity.Name;
            //register.CategoryId = entity.CategoryId;
            //register.Active = entity.Active;
            //register.ProductUrl = entity.ProductUrl;
            //register.UnitPrice = entity.UnitPrice;

            //_dbContext.Entry(entity).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();

            #endregion

            await Context.UpdateEntity(entity, Mapper);
        }

        public async Task DeleteAsync(string id)
        {
            #region Depacrated

            //var entity = await _dbContext.Set<Product>()
            //    .SingleOrDefaultAsync(c => c.Id == id);

            //if (entity == null) return;

            //_dbContext.Set<Product>().Remove(entity);
            //_dbContext.Entry(entity).State = EntityState.Deleted;
            //await _dbContext.SaveChangesAsync();
            #endregion

            await Context.DeleteEntity(new Product
            {
                Id = id
            });
        }


    }
}
