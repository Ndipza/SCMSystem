namespace Core.ViewModels
{
    public class OrderViewModel
    {
        public long ProductId { get; set; }

        public int Quantity { get; set; }
        public int CustomerId { get; set; } = 0;
    }
}
