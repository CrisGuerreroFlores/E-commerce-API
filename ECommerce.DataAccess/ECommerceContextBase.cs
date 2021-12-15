using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ECommerce.DataAccess
{
    public class ECommerceContextBase<TEntityBase>
       where TEntityBase : EntityBase, new()
    {
        protected readonly ECommerceDbContext Context;
        protected readonly IMapper Mapper;
        protected ECommerceContextBase(ECommerceDbContext context,
            IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<(ICollection<TEntityBase> collection, int total)>
            ListCollection(Expression<Func<TEntityBase, bool>> predicate,
                int pages, int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate)
                .OrderBy(p => p.Status)
                .AsNoTracking()
                .Skip((pages - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }
        public async Task<(ICollection<TInfo> collection, int total)>
            ListCollection<TInfo>(
                Expression<Func<TEntityBase,TInfo>> selector,
                Expression<Func<TEntityBase, bool>> predicate,
                int pages, 
                int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate)
                .OrderBy(p => p.Status)
                .AsNoTracking()
                .Select(selector)
                .Skip((pages - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }
        public async Task<(ICollection<TInfo> collection, int total)>
            ListCollection<TInfo>(
                Expression<Func<TEntityBase, TInfo>> selector,
                Expression<Func<TEntityBase, bool>> predicate,
                Expression<Func<TEntityBase, string>> orderBy,
                int pages,
                int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .Select(selector)
                .Skip((pages - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }
    }
}
