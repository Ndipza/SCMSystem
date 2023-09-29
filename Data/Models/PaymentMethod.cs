using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Description { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();
    }
}
