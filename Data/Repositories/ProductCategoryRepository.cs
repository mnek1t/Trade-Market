using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DbSet<ProductCategory> dbSet;
        public ProductCategoryRepository(TradeMarketDbContext context)
        {
            this.dbSet = context.Set<ProductCategory>();
        }
        public Task AddAsync(ProductCategory entity)
        {
           return this.dbSet.AddAsync(entity).AsTask();
        }

        public void Delete(ProductCategory entity)
        {
            this.dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public void Update(ProductCategory entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
