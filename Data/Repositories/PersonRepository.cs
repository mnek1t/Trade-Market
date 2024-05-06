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
    public class PersonRepository : IPersonRepository
    {
        private readonly DbSet<Person> dbSet;
        public PersonRepository(TradeMarketDbContext context)
        {
            this.dbSet = context.Set<Person>();
        }
        public async Task AddAsync(Person entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public void Delete(Person entity)
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

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public void Update(Person entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
