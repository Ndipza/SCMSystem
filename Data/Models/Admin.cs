namespace Data.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
