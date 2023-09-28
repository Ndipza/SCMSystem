using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CartStatusViewModel
    {
        [Required]
        [StringLength(50)]
        public string? Description { get; set; }
    }
}
