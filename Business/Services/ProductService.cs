using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        public Task AddAsync(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddCategoryAsync(ProductCategoryModel categoryModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategoryModel>> GetAllProductCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetByFilterAsync(FilterSearchModel filterSearch)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(ProductCategoryModel categoryModel)
        {
            throw new NotImplementedException();
        }
    }
}
