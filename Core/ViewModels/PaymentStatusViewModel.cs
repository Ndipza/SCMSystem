using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class PaymentStatusViewModel
    {
        [Required]
        [StringLength(50)]
        public string? Description { get; set; }
    }
}
