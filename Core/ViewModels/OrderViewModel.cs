using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class OrderViewModel
    {
        public long ProductId { get; set; }

        public int Quantity { get; set; }
        public int CustomerId { get; set; } = 0;
    }
}
