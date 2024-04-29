namespace QuickBank.Business.Exceptions
{
    public class AccountNotFoundException : QuickBankException
    {
        public AccountNotFoundException() { }
        public AccountNotFoundException(string message) : base(message) { }
    }
}
