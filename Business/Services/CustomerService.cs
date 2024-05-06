using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        public Task AddAsync(CustomerModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerModel>> GetCustomersByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
