namespace Core.ViewModels
{
    public class PaymentViewModel
    {
        public int CustomerId { get; set; }
        public int PaymentMethodID { get; set; }
        public int OrderItemId { get; set; }
        public decimal Amount { get; set; }
    }
}
