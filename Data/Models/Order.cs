namespace Data.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public virtual List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
