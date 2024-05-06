using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class StatisticService : IStatisticService
    {
        public StatisticService()
        {
            
        }
        public Task<IEnumerable<ProductModel>> GetCustomersMostPopularProductsAsync(int productCount, int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetIncomeOfCategoryInPeriod(int categoryId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetMostPopularProductsAsync(int productCount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerActivityModel>> GetMostValuableCustomersAsync(int customerCount, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
