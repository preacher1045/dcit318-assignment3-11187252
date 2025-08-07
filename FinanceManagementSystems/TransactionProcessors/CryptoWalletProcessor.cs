using CoreModels;

namespace TransactionProcessors
{
    public class CryptoWalletProcessor : ITransactionProcessor
    {
        private const int GasUnit = 21000; // working with the ETH transfer
        private const int BaseFee = 30; 
        private const int PriorityFee = 2;
        //private string PretendTransactionHash = "0x8a2e47fc1b3d45a9e9c40f";
        int randomNumber = 0;

        Random rand = new Random();

        //Hashset to store processed transactions
        private readonly HashSet<int> _ProcessedCryptoTransactionIds = new ();
        public void Process(Transaction transaction) 
        {

            Console.WriteLine("\nBeginning crypto transfer.....");

            if (transaction == null)
            {
                Console.WriteLine("Transaction not possible....");
                return;
            }

            if (transaction.Amount <= 0 || string.IsNullOrWhiteSpace(transaction.Category)) 
            {
                Console.WriteLine("Amount must be positive and Category must not be empty..");
                return;
            }

            if (_ProcessedCryptoTransactionIds.Contains(transaction.Id)) 
            {
                Console.WriteLine("Transaction has already been processed...");
                return;
            }

            int gasPriceTotal = BaseFee + PriorityFee;
            decimal totalGwei = Convert.ToDecimal(GasUnit * gasPriceTotal);
            decimal cryptoEquivalence = totalGwei / 1_000_000_000M;
            decimal cryptoValueInCurrency = cryptoEquivalence * transaction.Amount;

            // For testing only will delete later
            for (int i = 0; i < 7; i++) 
            {
                randomNumber = rand.Next(0, 101);
            }

            
            Console.WriteLine(
                $"Gas price: {gasPriceTotal}\n" +
                $"Total Gwei: {totalGwei}\n" +
                $"Crypto Equivalence: {cryptoEquivalence}\n" +
                $"Value in currency: {cryptoValueInCurrency}\n"
            );

            Console.WriteLine($"Transaction {randomNumber}\n\nTransfer of {cryptoValueInCurrency} for {transaction.Category} was processed successfully.... ");

            _ProcessedCryptoTransactionIds.Add(transaction.Id);


        }
    }
}