using System;
using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment from user account 
    /// </summary>
    public class WithdrawCommand : TransferCommand
    {
        public WithdrawCommand(string from, decimal amount)
        {
            TransferDescription = new TransferDescription(from, "", "", amount);
        }
    }
}