using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalCost { get; set; }
        public int? CartId { get; set; }
        public virtual Cart? Cart { get; set; }
        public int? ProductId { get; set;}
        public virtual Product? Product { get; set; }

    }
}
