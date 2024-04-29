namespace QuickBank.Business.Exceptions
{
    public class InvalidTransactionException : QuickBankException
    {
        public InvalidTransactionException() { }
        public InvalidTransactionException(string message) : base(message) { }
    }
}
