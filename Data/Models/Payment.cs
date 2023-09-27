namespace Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public int PaymentMethodID { get; set; }
        public int OrderItemId { get; set; }
        public decimal Amount { get; set; }
        
        public virtual Customer? Customer { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual OrderItem? OrderItem { get; set; }
    }
}
