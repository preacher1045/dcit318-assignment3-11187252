using CoreModels;

namespace TransactionProcessors
{
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        private const decimal DailyLimit = 2000m;        // Max allowed per transaction/day
        private const decimal TransferFee = 0.02m;       // 2% fee

        private readonly HashSet<int> _processedTransactionIds = new();

        public void Process(Transaction transaction)
        {
            Console.WriteLine("Beginning Mobile Money Transfer...");

            //  Validate transaction object
            if (transaction == null)
            {
                Console.WriteLine("Transaction not possible: transaction is null.");
                return;
            }

            //  Check if already processed
            if (_processedTransactionIds.Contains(transaction.Id))
            {
                Console.WriteLine($"Transaction ID {transaction.Id} has already been processed.");
                return;
            }

            //  Basic validations
            if (transaction.Amount <= 0 || string.IsNullOrWhiteSpace(transaction.Category))
            {
                Console.WriteLine("Amount must be positive and category must not be empty.");
                return;
            }

            //  Check daily limit
            if (transaction.Amount > DailyLimit)
            {
                Console.WriteLine($"Transaction exceeds daily limit of {DailyLimit:C}.");
                return;
            }

            // Calculate fee and net transfer
            decimal fee = transaction.Amount * TransferFee;
            decimal netAmount = transaction.Amount - fee;

            Console.WriteLine($"Transaction fee: {fee:C}. Net amount transferred: {netAmount:C}.");
            Console.WriteLine($"Transferred {netAmount:C} for {transaction.Category} via mobile money.");

            // Mark transaction as processed
            _processedTransactionIds.Add(transaction.Id);

            Console.WriteLine("Mobile money transaction completed successfully.\n");
        }
    }
}
