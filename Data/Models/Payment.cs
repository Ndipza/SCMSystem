using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public int? CartId { get; set; }
        public virtual Cart? Cart { get; set; }
        public int? PaymentMethodId { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public int? PaymentStatusId { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }

    }
}
