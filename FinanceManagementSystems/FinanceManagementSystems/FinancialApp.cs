using CoreModels;
using Accounts;
using TransactionProcessors;

namespace App
{
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new();

        public void Run()
        {
            // Create a SavingsAccount
            var savingsAccount = new SavingsAccount("ACC12345", 1000m);

            // Create three sample transactions
            var t1 = new Transaction(1, DateTime.Now, 50m, "Groceries");
            var t2 = new Transaction(2, DateTime.Now, 200m, "Utilities");
            var t3 = new Transaction(3, DateTime.Now, 100m, "Entertainment");

            // Process each transaction with a different processor
            var mobileMoneyProcessor = new MobileMoneyProcessor();
            var bankTransferProcessor = new BankTransferProcessor();
            var cryptoWalletProcessor = new CryptoWalletProcessor();

            mobileMoneyProcessor.Process(t1);
            bankTransferProcessor.Process(t2);
            cryptoWalletProcessor.Process(t3);

            // Apply each transaction to the account
            savingsAccount.ApplyTransaction(t1);
            savingsAccount.ApplyTransaction(t2);
            savingsAccount.ApplyTransaction(t3);

            // Add all transactions to _transactions list
            _transactions.AddRange(new[] { t1, t2, t3 });

            // Show final balance
            Console.WriteLine($"\nFinal balance: {savingsAccount.Balance:C}");

            // Show all transaction history
            Console.WriteLine("\nTransaction History:");
            foreach (var tx in _transactions)
            {
                Console.WriteLine($"ID: {tx.Id}, Date: {tx.Date}, Amount: {tx.Amount:C}, Category: {tx.Category}");
            }
        }

        public static void Main()
        {
            var app = new FinanceApp();
            app.Run();
        }
    }
}
