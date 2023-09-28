using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CustomerStatusViewModel
    {
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string? Description { get; set; }
    }
}
