using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
