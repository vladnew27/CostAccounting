namespace CostAccounting.Core
{
    [Serializable]
    public class TransactionException : Exception
    {
        public TransactionException()
        { }

        public TransactionException(string message)
            : base(message)
        { }

    }
}