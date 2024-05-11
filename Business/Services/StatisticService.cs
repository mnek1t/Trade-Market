using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public StatisticService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductModel>> GetCustomersMostPopularProductsAsync(int productCount, int customerId)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();
            
            if(receipts == null) 
            {
                throw new MarketException("Receipts are not found!");
            }

            var receiptDetails = receipts.SelectMany(receipt => receipt.ReceiptDetails);
            // get receipts of customer
            var productGroups = receiptDetails
                                .GroupBy(x => x.ProductId)
                                .Select(group => new { ProductId = group.Key, Quantity = group.Sum(x => x.Quantity) })
                                .OrderByDescending(x => x.Quantity)
                                .Take(productCount);

            var products = productGroups.Select(group => receiptDetails.First(x => x.ProductId == group.ProductId).Product);

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
        }

        public async Task<decimal> GetIncomeOfCategoryInPeriod(int categoryId, DateTime startDate, DateTime endDate)
        {

            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();
            var receiptDetails = receipts.Where(r => r.OperationDate > startDate && r.OperationDate < endDate)
                .SelectMany(x => x.ReceiptDetails)
                .Where(x => x.Product.ProductCategoryId == categoryId)
                .Select(x => x.DiscountUnitPrice * x.Quantity)
                .Sum();
            return receiptDetails;
        }

        public async Task<IEnumerable<ProductModel>> GetMostPopularProductsAsync(int productCount)
        {
            var receiptDetails = await _unitOfWork.ReceiptDetailRepository.GetAllWithDetailsAsync();

            if (receiptDetails == null)
            {
                throw new MarketException("Receipt Details null!");
            }

            var productGroups = receiptDetails
                .GroupBy(x => x.ProductId)
                .Select(group => new { ProductId = group.Key, Quantity = group.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .Take(productCount);

            var products = productGroups.Select(group => receiptDetails.First(x => x.ProductId == group.ProductId).Product);

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
        }

        public async Task<IEnumerable<CustomerActivityModel>> GetMostValuableCustomersAsync(int customerCount, DateTime startDate, DateTime endDate)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            if (receipts == null)
            {
                throw new MarketException("Receipts are not found!");
            }

            var receiptGroup = receipts
                .Where(x => x.OperationDate > startDate && x.OperationDate < endDate)
                .GroupBy(x =>x.Customer)
                .Select(receipt => new CustomerActivityModel { 
                    CustomerId = receipt.Key.Id, 
                    CustomerName = receipt.Key.Person.Name + ' ' + receipt.Key.Person.Surname,
                    ReceiptSum = receipt.SelectMany(x => x.ReceiptDetails).Select(x => x.Quantity * x.DiscountUnitPrice).Sum()
                })
                .OrderByDescending(x => x.ReceiptSum)
                .Take(customerCount);

            return receiptGroup;
        }
    }
}
