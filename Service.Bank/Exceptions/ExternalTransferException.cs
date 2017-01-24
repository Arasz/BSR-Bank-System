using System;

namespace Service.Bank.Exceptions
{
    public class ExternalTransferException : Exception
    {
        public int ErrorCode { get; }

        public ExternalTransferException(string message, int errorCode) : base(message + $"\n Received error code: {errorCode}")
        {
            ErrorCode = errorCode;
        }
    }
}