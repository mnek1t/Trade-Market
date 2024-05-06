using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Receipt : BaseEntity
    {
        public Receipt() : base() { }
        public Receipt(int id, int customerId, DateTime operationDate, bool isCheckeOut) : base(id)
        {
            this.CustomerId = customerId;
            this.OperationDate = operationDate;
            this.IsCheckedOut = isCheckeOut;

        }
        public DateTime OperationDate { get; set; }
        public bool IsCheckedOut { get; set; }
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; }
    }
}
