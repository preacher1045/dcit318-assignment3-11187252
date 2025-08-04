using CoreModels;
using System.Data.SqlTypes;

namespace TransactionProcessors
{
    public class BankTransferProcessor : ITransactionProcessor
    {
        // Initializing private variables

        private const decimal MaxTransferLimit = 10000m;
        private const decimal TransferFeeRate = 0.01m;
        private const decimal PretendBalance = 50000.0m;
        private readonly HashSet<int> _ProcessedTransactionIds = new();
        public void Process(Transaction transaction)
        {
            Console.WriteLine("Begining Bank Transfer Process.....");

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
            
            Console.WriteLine("Processing........");
            

            // Check if transaction has already been processed.
            if (_ProcessedTransactionIds.Contains(transaction.Id) )
            {
                Console.WriteLine($"Trasaction id: {transaction.Id} already processed..");
                return;
            }
            
            // Business logic.
            if (transaction.Amount >= MaxTransferLimit)
            {
                Console.WriteLine("Amount exceeds transfer limit...");
                return;

            }

            if (transaction.Amount >= PretendBalance)
            {
                Console.WriteLine("Insufficient Balance...");
                return;
            }

            // Calculate transaction fee and net amount to be transferred.
            decimal fee = transaction.Amount * TransferFeeRate;
            double netAmountTransferred = Convert.ToDouble(transaction.Amount - fee);
            Console.WriteLine($"Your transaction fee is {fee} \nYour net amount to be transferred would be: {netAmountTransferred}");

            Console.WriteLine($"Transferring {netAmountTransferred} for {transaction.Category} via bank transfer.");
            Console.WriteLine("Transaction completed successfully.....");

            // Add transaction id to  Hashset.
            _ProcessedTransactionIds.Add(transaction.Id);

        }

    }
}
