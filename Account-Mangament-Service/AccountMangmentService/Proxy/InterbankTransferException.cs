using System;

namespace Service.Bank.Proxy
{
    public class InterbankTransferException : Exception
    {
        public int ErrorCode { get; }

        public InterbankTransferException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}