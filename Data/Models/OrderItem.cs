namespace Data.Models
{
    public class OrderItem
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }


        public int StatusId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Status? Status { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }

    }
}
