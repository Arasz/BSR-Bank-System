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

        /// <summary>
        /// Charges account for external transfer 
        /// </summary>
        public override void HandleCommand(ExternalTransferChargeCommand command)
        {
            _transferDescription = command.TransferDescription;

            var chargedAmount = decimal.Round(command.ChargePercent * _transferDescription.Amount, 2);

            UpdateAccountBalance(chargedAmount, _transferDescription.SenderAccountNumber);

            CreateUpdatedDescription(chargedAmount);

            RegisterOperation();
        }

        private void CreateUpdatedDescription(decimal chargedAmount) => _transferDescription = new TransferDescription(_transferDescription.SenderAccountNumber, "", "", chargedAmount);
    }
}