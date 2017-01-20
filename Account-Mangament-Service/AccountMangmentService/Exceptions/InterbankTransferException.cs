using System;

namespace Service.Bank.Exceptions
{
    public class InterbankTransferException : Exception
    {
        public InterbankTransferException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}