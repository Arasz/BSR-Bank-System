using Data.Core;

namespace Service.Bank.Extensions
{
    public static class OperationExtension
    {
        public static void CalculateCreditOrDebit(this Operation operation)
        {
            var isSender = operation.Source == operation.Account.Number;

            if (isSender)
                operation.Debit = operation.Amount;
            else
                operation.Credit = operation.Amount;
        }
    }
}