using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("BankBranches")]
    public class BankBranch
    {
        [Key]
        public long BankBranchId { get; set; }

        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string ReservedCharacter { get; set; }

        public string BranchName { get; set; }
        public string BranchCode { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
    }
}
