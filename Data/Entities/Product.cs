using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        public Product(): base() { }
        public Product(int id, int productCategoryId, string productName, decimal price) : base(id)
        {
            this.ProductCategoryId = productCategoryId;
            this.ProductName = productName; 
            this.Price = price;
        }
        public int ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; }
    }
}
