using CoreModels;

namespace Accounts
{
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
                : base(accountNumber, initialBalance) { }
        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction == null) 
            {
                Console.WriteLine("Transaction cannot be null.");
            }
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insurfficient funds");
            }
            else 
            {
                Balance -= transaction.Amount;
                Console.WriteLine($"Transaction applied. New balance {Balance}");
            }
        }
    }
}
