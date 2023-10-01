using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Display(Description = "Image name")]
        [SwaggerSchema(ReadOnly = true)]
        public string? ImageName { get; set; }
        [Required]
        [Display(Description = "Category id")]
        public int CategoryId {  get; set; }  
        [Required]
        [NotMapped]        
        public IFormFile? ImageFile { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
