namespace Data.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Payment>? Payments { get; set; } = new List<Payment>();
    }
}
