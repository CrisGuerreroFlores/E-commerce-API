 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using AutoMapper;
 using ECommerce.DataAccess.IRepositories;
 using EcommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Repositories
{
    public class CategoryRepository : ECommerceContextBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        //paginacion de datos SQL Server
        public async Task<(ICollection<Category> collection, int total)>
            ListAsync(string filter, int page, int rows)
        {
            #region Depacrated

            //var collection = await _dbContext.Set<Category>()
            //    .Where(c => c.Name.StartsWith(filter) && c.Status)
            //    .OrderBy(c => c.Name )
            //    .AsNoTracking() //que cambios han ocurrido en los registros.
            //    .Skip((page - 1) * rows)
            //    .Take(rows)
            //    .ToListAsync();

            //var totalCount = await _dbContext.Set<Category>()
            //    .Where(c => c.Name.StartsWith(filter) && c.Status)
            //    .AsNoTracking()
            //    .CountAsync();

            //return (collection, totalCount);

            #endregion

            return await ListCollection(
                p=>p,
                p => p.Name.StartsWith(filter),
                p=>p.Description,
                page, 
                rows);
        }

        public async Task<Category> GetItemAsync(string id)
        {
            #region Depacrated

            // return await _dbContext.Set<Category>()
            //     .SingleOrDefaultAsync(c => c.Id == id);

            #endregion

            return await Context.Select<Category>(id);
        }

        public async Task<string> CreateAsync(Category entity)
        {
            #region Depacrated

            // await _dbContext.Set<Category>().AddAsync(entity);
            // _dbContext.Entry(entity).State = EntityState.Added;
            // await _dbContext.SaveChangesAsync();
            //
            // return entity.Id;

            #endregion

            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Category entity)
        {
            #region Depacrated

            // _dbContext.Set<Category>().Attach(entity);
            // _dbContext.Entry(entity).State = EntityState.Modified;
            // await _dbContext.SaveChangesAsync();

            #endregion

            await Context.UpdateEntity(entity, Mapper);
        }

        public async Task DeleteAsync(string id)
        {
            #region Depacrated

            // var entity = await _dbContext.Set<Category>()
            //     .SingleOrDefaultAsync(c => c.Id == id);
            //
            // if (entity == null) return;
            //
            // _dbContext.Set<Category>().Remove(entity);
            // _dbContext.Entry(entity).State = EntityState.Deleted;
            // await _dbContext.SaveChangesAsync();

            #endregion

            await Context.DeleteEntity(new Category
            {
                Id = id
            });

        }

       
    }
}
