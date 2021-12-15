using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ECommerce.DataAccess
{
    public static class DbContextExtensions
    {
        public static async Task<TEntityBase> Select<TEntityBase>(
            this DbContext context,
            string id) where TEntityBase : EntityBase
        {
            return await context.Set<TEntityBase>()
                .SingleOrDefaultAsync(x => x.Id == id && x.Status);
        }

        public static async Task<string> Insert<TEntityBase>
            (this DbContext context, TEntityBase entity)
        where TEntityBase : EntityBase
        {
            await context.Set<TEntityBase>().AddAsync(entity);
            context.Entry(entity).State = EntityState.Added;
            await context.SaveChangesAsync();

            return entity.Id;
        }

        public static async Task UpdateEntity<TEntityBase>(
            this DbContext context,
            TEntityBase entity, 
            IMapper mapper) 
            where TEntityBase : EntityBase
        {
            var register = await context.Set<TEntityBase>()
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (register == null) return;

            register = mapper.Map<TEntityBase>(entity);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteEntity<TEntityBase>(
            this DbContext context,
            TEntityBase entity) where TEntityBase : EntityBase
        {
            var register = await context.Set<TEntityBase>()
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if(register==null) return;

            entity.Status = false;

            context.Set<TEntityBase>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
