using System.ComponentModel.DataAnnotations;
namespace Core.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
