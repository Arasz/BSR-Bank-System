using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;

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

            UpdateAccountBalance(chargedAmount, _transferDescription.From);

            RegisterOperation();
        }
    }
}