using System;

namespace InterbankTransactionService.Exceptions
{
    public class IncorrectAccountNumberException : Exception
    {
        public bool ConcernsSenderAccountNumber { get; }

        public string IncorrectAccountNumber { get; }

        public override string Message => $"{AccountTypeName} account number is incorrect.\n" +
                                          $"Incorrect number {IncorrectAccountNumber}.\n" +
                                          $"Broken validation rule: {base.Message}\n";

        private string AccountTypeName => ConcernsSenderAccountNumber ? "Sender" : "Receiver";

        public IncorrectAccountNumberException(string incorrectAccountNumber, bool concernsSenderAccountNumber, string brokenValidationRule) : base(brokenValidationRule)
        {
            IncorrectAccountNumber = incorrectAccountNumber;
            ConcernsSenderAccountNumber = concernsSenderAccountNumber;
        }
    }
}