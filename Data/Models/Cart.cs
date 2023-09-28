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
        public CartStatus? CartStatus { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
    }
}
