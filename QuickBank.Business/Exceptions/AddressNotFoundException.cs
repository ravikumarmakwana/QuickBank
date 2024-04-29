namespace QuickBank.Business.Exceptions
{
    public class AddressNotFoundException : QuickBankException
    {
        public AddressNotFoundException() { }
        public AddressNotFoundException(string message) : base(message) { }
    }
}
