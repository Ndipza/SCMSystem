using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class CustomerViewModel
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        [StringLength(13)]
        public string? CellNumber { get; set; }
        [Required]
        public int CustomerStatusId { get; set; }
    }
}
