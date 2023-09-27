﻿namespace Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
