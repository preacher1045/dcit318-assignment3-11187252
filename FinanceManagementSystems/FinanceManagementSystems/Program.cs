using TransactionProcessors;
using CoreModels;

namespace FinanceManagementSystems
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var transaction = new Transaction(
                Id: 1,
                Date: DateTime.Now,
                Amount: 6500.00m,
                Category: "Utilities"
            );

            ITransactionProcessor processor = new CryptoWalletProcessor();
            processor.Process(transaction);



        }
    }
}
