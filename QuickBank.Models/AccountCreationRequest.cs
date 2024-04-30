namespace QuickBank.Models
{
    public class AccountCreationRequest
    {
        public long CustomerId { get; set; }
        public int AccountTypeId { get; set; }
        public string IFSC { get; set; }
    }
}
