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
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly DbSet<Receipt> dbSet;
        public ReceiptRepository(TradeMarketDbContext context)
        {
            this.dbSet = context.Set<Receipt>();
        }
        public Task AddAsync(Receipt entity)
        {
            return this.dbSet.AddAsync(entity).AsTask();
        }

        public void Delete(Receipt entity)
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

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();

        }

        public async Task<IEnumerable<Receipt>> GetAllWithDetailsAsync()
        {
            return await this.dbSet.Include(r => r.ReceiptDetails)
                .ThenInclude(rd => rd.Product)
                .ThenInclude(p=>p.Category)
                .Include(r => r.Customer)
                .ThenInclude(c => c.Person)
                .ToListAsync();
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public async Task<Receipt> GetByIdWithDetailsAsync(int id)
        {

            return await this.dbSet.Include(r => r.ReceiptDetails)
                                        .ThenInclude(rd => rd.Product)
                                            .ThenInclude(p => p.Category)
                                    .Include(r => r.Customer)
                                           .ThenInclude(c => c.Person).FirstOrDefaultAsync(r=> r.Id == id);
        }

        public void Update(Receipt entity)
        {
            this.dbSet.Update(entity);

        }
    }
}
