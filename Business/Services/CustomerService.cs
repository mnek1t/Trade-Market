using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(CustomerModel model)
        {
            if (model == null)
            {
                throw new MarketException("Attempt to create null object");

            }

            if (model.BirthDate.Year < 1900 || model.BirthDate.Year > DateTime.Now.Year)
            {
                throw new MarketException("Attempt to create null object");

            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new MarketException("Product name is empty!");
            }
            var customer = _mapper.Map<Customer>(model);
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.CustomerRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(customers);
        }

        public async  Task<CustomerModel> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdWithDetailsAsync(id);
            if (customer == null)
            {
                throw new MarketException("customer is null");
            }

            return _mapper.Map<Customer, CustomerModel>(customer);
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomersByProductIdAsync(int productId)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync();
            if (customers == null)
            {
                throw new MarketException("Customers are not found");
            }
            var filteredCustomers = customers.Where(c => c.Receipts
                                                            .Any(r => r.ReceiptDetails
                                                            .Any(rd => rd.ProductId == productId)))
                                                .ToList();
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(filteredCustomers);
        }

        public async Task UpdateAsync(CustomerModel model)
        {
            if (model == null)
            {
                throw new MarketException("Attempt to create null object");

            }
            if (string.IsNullOrEmpty(model.Surname))
            {
                throw new MarketException("Product name is empty!");
            }
            if (model.BirthDate.Year < 1900 || model.BirthDate.Year > DateTime.Now.Year)
            {
                throw new MarketException("Attempt to create null object");

            }
            var customer = _mapper.Map<Customer>(model);
            _unitOfWork.CustomerRepository.Update(customer);
            await _unitOfWork.SaveAsync();
        }
    }
}
