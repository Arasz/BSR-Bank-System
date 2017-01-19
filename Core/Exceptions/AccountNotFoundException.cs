using System;

namespace Service.Bank.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string message) : base(message)
        {
        }
    }
}