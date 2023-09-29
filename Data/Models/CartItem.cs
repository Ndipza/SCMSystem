using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int? CartId { get; set; }
        public virtual Cart? Cart { get; set; }
        public int? ProductId { get; set;}
        public virtual Product? Product { get; set; }

    }
}
