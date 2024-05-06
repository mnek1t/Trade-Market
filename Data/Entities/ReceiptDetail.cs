using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ReceiptDetail:BaseEntity
    {
        public ReceiptDetail() : base() { }
        public ReceiptDetail(int id, int receiptId, int productid, int quantity, decimal discountUnitPrice, decimal unitPrice) : base(id)
        {
            this.ReceiptId = receiptId;
            this.ProductId = productid;
            this.Quantity = quantity;
            this.DiscountUnitPrice = discountUnitPrice;
            this.UnitPrice = unitPrice;
        }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public Receipt Receipt { get; set; }
        public Product Product { get; set; }
        
    }
}
