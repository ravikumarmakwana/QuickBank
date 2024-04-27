using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public long AddressId { get; set; }

        public Customer Customer { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
    }
}
