using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
