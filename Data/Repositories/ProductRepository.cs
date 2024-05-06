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
    public class ProductRepository : IProductRepository
    {

        private readonly DbSet<Product> dbSet;
        public ProductRepository(TradeMarketDbContext context)
        {
            this.dbSet = context.Set<Product>();
        }
        public async Task AddAsync(Product entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(Product entity)
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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await this.dbSet.Include(p => p.Category)
                .Include(p => p.ReceiptDetails)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public async Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return await this.dbSet.Include(p => p.Category)
                .Include(p => p.ReceiptDetails)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Product entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
