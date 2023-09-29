﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public byte[] Image { get; set; } = Array.Empty<byte>();
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }        
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<CartItem>? CartItems { get; set; } = new List<CartItem>();
    }
}
