﻿using CoreModels;

namespace Accounts
{
    public class Account
    {
        public string AccountNumber { get; init; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance) 
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction) 
        {
            Balance -= transaction.Amount;
        }
    }
}
