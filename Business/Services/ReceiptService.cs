using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Validation;

namespace Business.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ReceiptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(ReceiptModel model)
        {
            var receipt = _mapper.Map<Receipt>(model);
            await _unitOfWork.ReceiptRepository.AddAsync(receipt);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddProductAsync(int productId, int receiptId, int quantity)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId);

            if (receipt == null)
            {
                throw new MarketException("Receipt is null!");
            }
            if (_unitOfWork.ProductRepository != null)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

                if (product == null)
                {
                    throw new MarketException("Product is null!");
                }
                var model = new ReceiptDetailModel
                {
                    ReceiptId = receiptId,
                    ProductId = productId,
                    Quantity = quantity,
                    DiscountUnitPrice = product.Price * (1 - (decimal)receipt.Customer.DiscountValue / 100),
                    UnitPrice = product.Price
                };
                await _unitOfWork.ReceiptDetailRepository.AddAsync(_mapper.Map<ReceiptDetail>(model));
            }
            else 
            {
                var receiptDetail = receipt.ReceiptDetails.FirstOrDefault(x => x.ProductId == productId);
                if (receiptDetail != null)
                {
                    receiptDetail.Quantity += quantity;
                }
            }



            await _unitOfWork.SaveAsync();
        }

        public async Task CheckOutAsync(int receiptId)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdAsync(receiptId);
            if (receipt == null)
            {
                throw new MarketException($"Receipt with id {receiptId} does not exist.");
            }

            receipt.IsCheckedOut = true;
            _unitOfWork.ReceiptRepository.Update(receipt);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(modelId);
            if (receipt == null)
            {
                throw new MarketException($"Receipt with id does not exist.");
            }
            foreach (var detail in receipt.ReceiptDetails)
            {
                _unitOfWork.ReceiptDetailRepository.Delete(detail);
            }
            await _unitOfWork.ReceiptRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ReceiptModel>> GetAllAsync()
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            if (receipts == null)
            {
                throw new MarketException("Reciepts is null");
            }

            return _mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptModel>>(receipts);
        }

        public async Task<ReceiptModel> GetByIdAsync(int id)
        {

            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(id);
            if (receipt == null)
            {
                // Handle the case when the receipt with the given id does not exist
                throw new MarketException("reciept is null");
            }

            return _mapper.Map<Receipt, ReceiptModel>(receipt);
        }

        public async Task<IEnumerable<ReceiptDetailModel>> GetReceiptDetailsAsync(int receiptId)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId);
            if (receipt == null)
            {
                throw new MarketException("reciept is null");
            }

            var receiptDetails = receipt.ReceiptDetails;
            return _mapper.Map<IEnumerable<ReceiptDetail>, IEnumerable<ReceiptDetailModel>>(receiptDetails);
        }
         
        public async Task<IEnumerable<ReceiptModel>> GetReceiptsByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();
            var filteredReceipts = receipts.Where(r => r.OperationDate >= startDate && r.OperationDate <= endDate);

            return _mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptModel>>(filteredReceipts);

        }

        public async Task RemoveProductAsync(int productId, int receiptId, int quantity)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId);

            if (receipt == null)
            {
                throw new MarketException("Receipt is null!");
            }
            var receiptDetails = receipt.ReceiptDetails.FirstOrDefault(x => x.ProductId == productId);
            if (receiptDetails == null)
            {
                throw new MarketException("Receipt Deatails is null!");
            }
            receiptDetails.Quantity -= quantity;
            if (receiptDetails.Quantity == 0) 
            {
                _unitOfWork.ReceiptDetailRepository.Delete(receiptDetails);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<decimal> ToPayAsync(int receiptId)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId);

            decimal total = 0;
            foreach (var detail in receipt.ReceiptDetails)
            {
                total += detail.Quantity * detail.DiscountUnitPrice;
            }
            return total;
        }

        public async Task UpdateAsync(ReceiptModel model)
        {
            var receipt = _mapper.Map<ReceiptModel, Receipt>(model);
            _unitOfWork.ReceiptRepository.Update(receipt);
            await _unitOfWork.SaveAsync();
        }
    }
}
