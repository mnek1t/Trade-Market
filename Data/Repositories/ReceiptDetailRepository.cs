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
    public class ReceiptDetailRepository : IReceiptDetailRepository
    {
        private readonly DbSet<ReceiptDetail> dbSet;
        public ReceiptDetailRepository(TradeMarketDbContext context)
        {
            this.dbSet = context.Set<ReceiptDetail>();
        }
        public Task AddAsync(ReceiptDetail entity)
        {
           return this.dbSet.AddAsync(entity).AsTask();
        }

        public void Delete(ReceiptDetail entity)
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

        public async Task<IEnumerable<ReceiptDetail>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();

        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await this.dbSet.Include(rd => rd.Product)
                .ThenInclude(p => p.Category)
                .Include(rd => rd.Receipt)
                .ToListAsync();
        }

        public async Task<ReceiptDetail> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public void Update(ReceiptDetail entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
