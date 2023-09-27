namespace Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Payment>? Payments { get; set; }
        public virtual List<Cart>? Carts { get; set; } = new List<Cart>();
    }
}
