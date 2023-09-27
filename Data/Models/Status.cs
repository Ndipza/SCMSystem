﻿namespace Data.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
