using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
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
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CustomerStatusId { get; set; }
        public virtual CustomerStatus? CustomerStatus { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; } = new List<Cart>();
    }
}
