using CoreModels;

namespace TransactionProcessors
{
    public class CryptoWalletProcessor : ITransactionProcessor
    {
        private const int GasUnit = 21000; // working with the ETH transfer
        private const int BaseFee = 30; // in gwei
        private const int PriorityFee = 2; // in gwei
        private const decimal PretendAmount = 450000.00m; // Pretend Value
        private string PretendTransactionHash = "0x8a2e47fc1b3d45a9e9c40f";
        int randomNumber = 0;

        Random rand = new Random();

        //Hashset to store processed transactions
        private readonly HashSet<int> _ProcessedCryptoTransactionIds = new ();
        public void Process(Transaction transaction) 
        {

            if (transaction == null)
            {
                Console.WriteLine("Transaction not possible...");
                return;
            }

            if (transaction.Amount <= 0 || string.IsNullOrWhiteSpace(transaction.Category)) 
            {
                Console.WriteLine("Amount must be positive and Category must not be empty..");
                return;
            }

            Console.WriteLine("Processing.......");

            if (_ProcessedCryptoTransactionIds.Contains(transaction.Id)) 
            {
                Console.WriteLine("Transaction has already been processed...");
                return;
            }

            int gasPriceTotal = BaseFee + PriorityFee;
            decimal totalGwei = Convert.ToDecimal(GasUnit * gasPriceTotal);
            decimal cryptoEquivalence = totalGwei / 1_000_000_000M;
            decimal cryptoValueInCurrency = cryptoEquivalence * transaction.Amount;

            if (cryptoValueInCurrency >= PretendAmount) 
            {
                Console.WriteLine("Insurfficient funds...");
                return;
            }

            // For testing only will delete later
            for (int i = 0; i < 7; i++) 
            {
                randomNumber = rand.Next(0, 101);
            }

            
            Console.WriteLine(
                $"Gas price: {randomNumber}\n" +
                $"Total Gwei: {transaction.Category}\n" +
                $"Crypto Equivalence: {cryptoEquivalence}\n" +
                $"Value in currency: {cryptoValueInCurrency}\n"
            );

            Console.WriteLine($"Transaction {randomNumber}\n\n Transfer of {cryptoValueInCurrency} for {transaction.Category} was processed successfully.... ");

            _ProcessedCryptoTransactionIds.Add(transaction.Id);


        }
    }
}