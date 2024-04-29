using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class UpdateAddressRequest
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }

        public AddressType AddressType { get; set; }
    }
}
