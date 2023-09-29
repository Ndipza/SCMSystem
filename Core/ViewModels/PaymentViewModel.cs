namespace Core.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentMethodID { get; set; }
        public int CartId { get; set; }
        public int PaymentStatusId { get; set; }
        public decimal Amount { get; set; }

    }
}
