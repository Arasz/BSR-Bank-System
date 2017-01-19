using System;
using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment from user account 
    /// </summary>
    public class WithdrawCommand : TransferCommand
    {
        //TODO add from bank number and title
        public WithdrawCommand(string to, decimal amount)
        {
            TransferDescription = new TransferDescription("from", to, "title", amount);
        }
    }
}