using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CustomerViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(13)]
        public string CellNumber { get; set; }
    }
}
