﻿using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CustomerStatus
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Description { get; set; }
        public ICollection<Customer>? Customers { get; set; } = new List<Customer>();
    }
}