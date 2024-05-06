using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductCategory : BaseEntity
    {
        public ProductCategory() : base() { }
        public ProductCategory(int id, string categoryName) : base(id)
        {
            this.CategoryName = categoryName;
        }
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; }
    }
}
