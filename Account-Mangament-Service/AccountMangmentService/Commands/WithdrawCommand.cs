using System;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment from user account 
    /// </summary>
    public class WithdrawCommand : TransferCommand
    {
        public WithdrawCommand(string sourceAccountNumber, decimal amount)
        {
            From = sourceAccountNumber;
            Amount = amount;
        }
    }
}