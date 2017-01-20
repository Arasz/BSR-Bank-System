using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Bank.Proxy;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.CommandHandlers.External
{
    public class ExternalTransferCommandHandler : BankOperationCommandHandler<ExternalTransferCommand>
    {
        private readonly ICommandBus _commandBus;
        private readonly BankDataContext _dataContext;
        private readonly IInterbankTransferServiceProxy _interbankTransferService;

        public ExternalTransferCommandHandler(BankDataContext dataContext, IInterbankTransferServiceProxy interbankTransferService, ICommandBus commandBus, IOperationRegister operationRegister) : base(dataContext, operationRegister)
        {
            _interbankTransferService = interbankTransferService;
            _commandBus = commandBus;
        }

        public override void HandleCommand(ExternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

            MakeInterbankTransfer();

            UpdateAccountBalance(_transferDescription.Amount, _transferDescription.From);

            RegisterOperation();

            _commandBus.Send(new ExternalTransferChargeCommand(command.TransferDescription));
        }

        private void MakeInterbankTransfer()
        {
            var interbankTransferDescription = (InterbankTransferDescription)_transferDescription;
            _interbankTransferService.Transfer(interbankTransferDescription);
        }
    }
}