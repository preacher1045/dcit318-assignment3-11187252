using CoreModels;

namespace TransactionProcessors
{
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}
