using System;
using System.Collections.Generic;
using CQRS.Queries;
using Data.Core;

namespace Service.Bank.Queries
{
    /// <summary>
    /// Accounts operations history query 
    /// </summary>
    public class AccountHistoryQuery : IQuery<IEnumerable<Operation>>
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        private string AccountNumber { get; set; }

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