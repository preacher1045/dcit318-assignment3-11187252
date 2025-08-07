using CoreModels;

namespace TransactionProcessors
{
    public class BankTransferProcessor : ITransactionProcessor
    {
        // Initializing private variables

        private const decimal MaxTransferLimit = 10000m;
        private const decimal TransferFeeRate = 0.01m;
        private readonly HashSet<int> _ProcessedTransactionIds = new();
        public void Process(Transaction transaction)
        {
            Console.WriteLine("Beginning Bank Transfer Process.....");

            // Check if transaction params passed into the proces method is null.
            if (transaction == null)
            {
                Console.WriteLine("Transaction not possible..");
                return;
            }

            

            // Check if Amount is less than or equal to 0 OR Category is Null, Empty, or is just whitespace
            if (transaction.Amount <= 0 || string.IsNullOrWhiteSpace(transaction.Category))
            {
                Console.WriteLine("Amount must be positive and Category must not be empty..");
                return;
            } 
           

            // Check if transaction has already been processed.
            if (_ProcessedTransactionIds.Contains(transaction.Id))
            {
                Console.WriteLine($"Transaction id: {transaction.Id} already processed..");
                return;
            }

            // Business logic.
            if (transaction.Amount >= MaxTransferLimit)
            {
                Console.WriteLine("Amount exceeds transfer limit...");
                return;

            }

            // Calculate transaction fee and net amount to be transferred.
            decimal fee = transaction.Amount * TransferFeeRate;
            decimal netAmountTransferred = transaction.Amount - fee;
            Console.WriteLine($"Your transaction fee is {fee} \nYour net amount to be transferred would be: {netAmountTransferred}");

            Console.WriteLine($"Transferring {netAmountTransferred} for {transaction.Category} via bank transfer.");
            Console.WriteLine("Transaction completed successfully.....");

            // Add transaction id to  Hashset.
            _ProcessedTransactionIds.Add(transaction.Id);

        }

    }
}
