using System;

namespace InterbankTransactionService.Exceptions
{
    public class TransferAmountException : Exception
    {
        public int IncorrectAmount { get; }

        public override string Message => $"Amount {IncorrectAmount} is incorrect." +
               $" Broken validation rule: {base.Message}";

        public TransferAmountException(int incorrectAmount, string brokenValidationRule) : base(brokenValidationRule)
        {
            IncorrectAmount = incorrectAmount;
        }
    }
}