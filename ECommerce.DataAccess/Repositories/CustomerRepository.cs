using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.DataAccess.IRepositories;

namespace ECommerce.DataAccess.Repositories
{
    public class CustomerRepository : ECommerceContextBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext context, IMapper mapper) 
            : base(context, mapper)
        {

        }

        public async Task<(ICollection<Customer> collection, int total)>
            ListAsync(string filter, int pages, int rows)
        {
            #region Depacrated

            //var collection = await _dbContext.Set<Customer>()
            //    .Where(p => p.Name.StartsWith(filter) && p.Status)
            //    .OrderBy(c => c.Name)
            //    .AsNoTracking() //que cambios han ocurrido en los registros.
            //    .Skip((page - 1) * rows)
            //    .Take(rows)
            //    .ToListAsync();

            //var totalCount = await _dbContext.Set<Customer>()
            //    .Where(c => c.Name.StartsWith(filter) && c.Status)
            //    .AsNoTracking()
            //    .CountAsync();

            //return (collection, totalCount);

            #endregion

            return await ListCollection
            (p => p.Name.StartsWith(filter)
                      && p.Status, pages, rows);
        }

        public async Task<Customer> GetItemAsync(string id)
        {
            return await Context.Set<Customer>()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetItemByEmailAsync(string email)
        {
            return await Context.Set<Customer>()
                .Where(c => c.Email.Equals(email))
                .SingleOrDefaultAsync();
        }

        public async Task<string> CreateAsync(Customer entity)
        {
            #region Depacrated

            //await _dbContext.Set<Customer>().AddAsync(entity);
            //_dbContext.Entry(entity).State = EntityState.Added;
            //await _dbContext.SaveChangesAsync();

            //return entity.Id;

            #endregion

            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Customer entity)
        {
            #region Depacrated

            //_dbContext.Set<Customer>().Attach(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
            #endregion

            await Context.UpdateEntity(entity, Mapper);
        }

        public async Task DeleteAsync(string id)
        {
            #region Depacrated

            //var entity = await _dbContext.Set<Customer>()
            //    .SingleOrDefaultAsync(c => c.Id == id);

            //if (entity == null) return;

            //_dbContext.Set<Customer>().Remove(entity);
            //_dbContext.Entry(entity).State = EntityState.Deleted;
            //await _dbContext.SaveChangesAsync();

            #endregion

            await Context.DeleteEntity(new Customer
            {
                Id = id
            });
        }

     
    }
}
