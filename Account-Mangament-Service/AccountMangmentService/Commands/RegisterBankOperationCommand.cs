using CQRS.Commands;
using Service.Dto;

namespace Service.Bank.Commands
{
    public class RegisterBankOperationCommand : ICommand
    {
        public decimal AccountBalance { get; }

        /// <summary>
        /// Ma 
        /// </summary>
        public decimal Credit { get; }

        /// <summary>
        /// Winnien 
        /// </summary>
        public decimal Debit { get; }

        public string SourceCommandName { get; }

        public TransferDescription TransferDescription { get; }

        public RegisterBankOperationCommand(TransferDescription transferDescription, decimal accountBalance, string sourceCommandName, decimal credit, decimal debit)
        {
            TransferDescription = transferDescription;
            AccountBalance = accountBalance;
            SourceCommandName = sourceCommandName;
            Credit = credit;
            Debit = debit;
        }
    }
}