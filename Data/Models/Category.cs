using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string? Name { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
