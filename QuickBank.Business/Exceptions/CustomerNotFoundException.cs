namespace QuickBank.Business.Exceptions
{
    public class CustomerNotFoundException : QuickBankException
    {
        public CustomerNotFoundException() { }
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
