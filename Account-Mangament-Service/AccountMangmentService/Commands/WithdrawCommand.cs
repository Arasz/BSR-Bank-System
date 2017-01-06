using System;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment from user account 
    /// </summary>
    public class WithdrawCommand : TransferCommand
    {
        public WithdrawCommand(string from, decimal amount)
        {
            From = from;
            Amount = amount;
        }
    }
}