using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class OrderItemViewModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }


        public int StatusId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
