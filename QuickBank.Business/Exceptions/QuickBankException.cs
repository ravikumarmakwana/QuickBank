namespace QuickBank.Business.Exceptions
{
    public class QuickBankException : ApplicationException
    {
        public QuickBankException() { }
        public QuickBankException(string message) : base(message) { }
        public QuickBankException(string message, Exception innerException) : base(message, innerException) { }
    }
}
