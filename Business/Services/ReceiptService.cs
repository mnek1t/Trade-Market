using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ReceiptService : IReceiptService
    {
        public Task AddAsync(ReceiptModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddProductAsync(int productId, int receiptId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task CheckOutAsync(int receiptId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReceiptModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReceiptModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReceiptDetailModel>> GetReceiptDetailsAsync(int receiptId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReceiptModel>> GetReceiptsByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProductAsync(int productId, int receiptId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> ToPayAsync(int receiptId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ReceiptModel model)
        {
            throw new NotImplementedException();
        }
    }
}
