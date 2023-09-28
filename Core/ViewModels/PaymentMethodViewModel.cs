using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class PaymentMethodViewModel
    {
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string? Name { get; set; }
    }
}
