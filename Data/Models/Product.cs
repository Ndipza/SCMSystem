using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public virtual Category? Category { get; set; }
        [Required]
        public int AdminId { get; set; }
        [Required]
        public virtual Admin? Admin { get; set; }
        public virtual List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
