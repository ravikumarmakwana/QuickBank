namespace QuickBank.Business.Helpers
{
    public static class Generator
    {
        private const int _sizeOfAccountNumber = 14;
        private const string _prefix = "Quick";
        private const bool _allowPrefix = true;

        public static string GenerateNewAccountNumber(List<string> accountNumbers)
        {
            string accountNumber;
            var size = _sizeOfAccountNumber;

            if (_allowPrefix)
            {
                size -= _prefix.Length;
                accountNumber = _prefix;
            }

            var generator = new Random();

            while (true)
            {
                for (int i = 0; i < size; i++)
                {
                    accountNumber += generator.Next(0, 9).ToString();
                }
                if (!accountNumbers.Contains(accountNumber))
                {
                    return accountNumber;
                }
                accountNumber = _prefix;
            }
        }

        public static string EncryptTransactionId(long transactionId)
        {
            return DateTime.Now.ToString("ddMMyyyy") + transactionId;
        }

        public static long DecryptTransactionId(string referenceNumber)
        {
            return long.Parse(referenceNumber.Substring(8));
        }
    }
}
