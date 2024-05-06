using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customer: BaseEntity
    {
        public Customer() : base() { }
        public Customer(int id, int personId, int discountValue) : base(id)
        {
            this.DiscountValue = discountValue;
            this.PersonId = personId;
        }
        public int DiscountValue { get; set; }
        public int PersonId { get; set; }
        
        public Person Person { get; set; }
        public ICollection<Receipt> Receipts { get; }
    }
}
