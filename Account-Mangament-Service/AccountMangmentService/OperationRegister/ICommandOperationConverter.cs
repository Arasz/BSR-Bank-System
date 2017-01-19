using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.OperationRegister
{
    public interface ICommandOperationConverter
    {
        Operation Convert(RegisterBankOperationCommand registerBankOperationCommand);
    }
}