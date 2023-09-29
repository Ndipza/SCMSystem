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
        public string? Name { get; set; }
        [Required]
        public byte[] Image { get; set; } = Array.Empty<byte>();
        public int Price { get; set; }
    }
}
