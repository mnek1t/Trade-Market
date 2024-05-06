using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbSet<Customer> dbSet;
        public CustomerRepository(TradeMarketDbContext context)
        {
            this.dbSet = context.Set<Customer>();
        }
        public Task AddAsync(Customer entity)
        {
            return this.dbSet.AddAsync(entity).AsTask();
        }

        public void Delete(Customer entity)
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

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await this.dbSet.Include(p => p.Receipts)
                .ThenInclude(r => r.ReceiptDetails)
                .Include(p => p.Person).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity != null) 
            {
                return entity;
            }
            return null;
        }

        public async Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            return await this.dbSet.Include(p => p.Receipts)
                .ThenInclude(r => r.ReceiptDetails)
                .Include(p => p.Person).FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Customer entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
