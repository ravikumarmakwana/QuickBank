using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class AddressModel
    {
        public long AddressId { get; set; }
        public long CustomerId { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }

        public AddressType AddressType { get; set; }
    }
}
