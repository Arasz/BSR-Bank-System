using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Dto;

namespace Service.Bank.CommandHandlers.External
{
    public class ExternalTransferChargeCommandHandler : BankOperationCommandHandler<ExternalTransferChargeCommand>
    {
        private readonly BankDataContext _dataContext;

        public ExternalTransferChargeCommandHandler(BankDataContext dataContext, IOperationRegister operationRegister)
            : base(dataContext, operationRegister)
        {
        }

        public override void HandleCommand(ExternalTransferChargeCommand command)
        {
            _transferDescription = command.TransferDescription;

            var chargedAmount = command.ChargePercent * _transferDescription.Amount;

            UpdateAccountBalance(chargedAmount, _transferDescription.SourceAccountNumber);

            CreateUpdatedDescription(chargedAmount);

            RegisterOperation();
        }

        private void CreateUpdatedDescription(decimal chargedAmount) => _transferDescription = new TransferDescription(_transferDescription.SourceAccountNumber, "", "", chargedAmount);
    }
}