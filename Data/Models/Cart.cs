using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CartStatusId { get; set; }        
        public virtual CartStatus? CartStatus { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public virtual ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
    }
}
