using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class PaymentStatusViewModel
    {
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string? Description { get; set; }
    }
}
