using System;
using System.Collections.Generic;
using CQRS.Queries;
using Data.Core;

namespace Service.Dto
{
    public class AccountHistoryQuery
    {
        public string AccountNumber { get; }

        public DateTime From { get; }

        public DateTime To { get; }

        public AccountHistoryQuery(DateTime from, DateTime to, string accountNumber)
        {
            From = from;
            To = to;
            AccountNumber = accountNumber;
        }

        public AccountHistoryQuery(string accountNumber) : this(DateTime.MinValue, DateTime.MaxValue, accountNumber)
        {
        }
    }
}