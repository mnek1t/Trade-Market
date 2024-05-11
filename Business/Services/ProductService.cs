using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(ProductModel model)
        {
            if (model != null && model.Price < 0)
            {
                throw new MarketException("'price cannot be negative!");

            }

            if (string.IsNullOrEmpty(model.ProductName))
            {
                throw new MarketException("Product name is empty!");
            }
            var product = _mapper.Map<Product>(model);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async  Task AddCategoryAsync(ProductCategoryModel categoryModel)
        {
            if (string.IsNullOrEmpty(categoryModel.CategoryName)) 
            {
                throw new MarketException("Category name is empty!");
            }
            var productCategory = _mapper.Map<ProductCategory>(categoryModel);
            await _unitOfWork.ProductCategoryRepository.AddAsync(productCategory);
            await _unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int modelId)
        {
            return _unitOfWork.ProductRepository.DeleteByIdAsync(modelId).ContinueWith(t => _unitOfWork.SaveAsync());
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithDetailsAsync();
            if (products == null ) 
            {
                throw new MarketException("Product are not found");
            }

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
        }

        public async Task<IEnumerable<ProductCategoryModel>> GetAllProductCategoriesAsync()
        {
            var productCategories = await _unitOfWork.ProductCategoryRepository.GetAllAsync();
            if (productCategories == null)
            {
                throw new MarketException("Product categories are not found");
            }

            return _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryModel>>(productCategories);
        }

        public async Task<IEnumerable<ProductModel>> GetByFilterAsync(FilterSearchModel filterSearch)
        {
            // Get all products
            var products = await _unitOfWork.ProductRepository.GetAllWithDetailsAsync();

            // Filter by CategoryId if it is not null
            if (filterSearch.CategoryId.HasValue)
            {
                products = products.Where(p => p.ProductCategoryId == filterSearch.CategoryId.Value);
            }

            // Filter by MinPrice if it is not null
            if (filterSearch.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= filterSearch.MinPrice.Value);
            }

            // Filter by MaxPrice if it is not null
            if (filterSearch.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= filterSearch.MaxPrice.Value);
            }

            // Map to ProductModel
            var productModels = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

            return productModels;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<Product, ProductModel>(product);
        }

        public async  Task RemoveCategoryAsync(int categoryId)
        {
            await _unitOfWork.ProductCategoryRepository.DeleteByIdAsync(categoryId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ProductModel model)
        {
            if (model == null)
            {
                throw new MarketException("Try to update produt category by null !");
            }
            if (string.IsNullOrEmpty(model.ProductName))
            {
                throw new MarketException("Category with empty name!");
            }

            var product = _mapper.Map<Product>(model);
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(ProductCategoryModel categoryModel)
        {
            if (categoryModel == null) 
            {
                throw new MarketException("Try to update produt category by null !");
            }
            if (string.IsNullOrEmpty(categoryModel.CategoryName)) 
            {
                throw new MarketException("Category with empty name!");
            }
            var productCategory = _mapper.Map<ProductCategory>(categoryModel);
            _unitOfWork.ProductCategoryRepository.Update(productCategory);
            await _unitOfWork.SaveAsync();
        }
    }
}
