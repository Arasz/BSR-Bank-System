using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers.External
{
    public class ExternalTransferChargeCommandHandler : ICommandHandler<ExternalTransferChargeCommand>
    {
        private readonly BankDataContext _dataContext;

        public ExternalTransferChargeCommandHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void HandleCommand(ExternalTransferChargeCommand command)
        {
            var transferDescription = command.TransferDescription;

            var chargedAmount = command.ChargePercent * transferDescription.Amount;

            UpdateUserAccountBalance(chargedAmount, transferDescription.From);
        }

        private void UpdateUserAccountBalance(decimal chargeAmount, string accountNumber)
        {
            var chargedAccount = _dataContext.Accounts.Single(account => account.Number == accountNumber);

            chargedAccount.Balance -= chargeAmount;

            _dataContext.SaveChanges();
        }
    }
}