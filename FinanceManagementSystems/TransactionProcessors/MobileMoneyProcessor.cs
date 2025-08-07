using CoreModels;

namespace TransactionProcessors
{
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        private readonly HashSet<int> _ProcessedTransactionsIds = new();
        public void Process(Transaction transaction)
        {
            Console.WriteLine("Hello world");
            // Logic for pin auth


            Console.Write("Enter recipient's number: ");
            string input = Console.ReadLine();

            int recipientsNumber = Convert.ToInt32(input);


        }
    }
}
