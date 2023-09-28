using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CartStatus
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Description { get; set; }
        public ICollection<Cart>? Carts { get; set;} = new List<Cart>();
    }
}
