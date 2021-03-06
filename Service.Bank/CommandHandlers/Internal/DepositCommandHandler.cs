﻿using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.Internal
{
    public class DepositCommandHandler : BankOperationCommandHandler<DepositCommand>

    {
        public DepositCommandHandler(BankDataContext bankDataContext, IOperationRegister register)
            : base(bankDataContext, register)
        {
        }

        /// <summary>
        /// Makes deposit 
        /// </summary>
        public override void HandleCommand(DepositCommand command)
        {
            _transferDescription = command.TransferDescription;

            UpdateAccountBalance(_transferDescription.Amount, _transferDescription.ReceiverAccountNumber);

            RegisterOperation();
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance += amount;
    }
}