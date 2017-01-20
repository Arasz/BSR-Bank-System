using System;
using System.Collections.Generic;
using Core.CQRS.Queries;
using Data.Core;
using Service.Dto;

namespace Service.Bank.Queries
{
    /// <summary>
    ///     Accounts operations history query
    /// </summary>
    public class AccountOperationsHistoryQuery : IQuery<IEnumerable<Operation>>
    {
        public AccountOperationsHistoryQuery(AccountHistoryQuery accountHistoryQuery)
        {
            From = accountHistoryQuery.From;
            To = accountHistoryQuery.To;
            AccountNumber = accountHistoryQuery.AccountNumber;
        }

        public AccountOperationsHistoryQuery(DateTime from, DateTime to, string accountNumber)
        {
            From = from;
            To = to;
            AccountNumber = accountNumber;
        }

        public AccountOperationsHistoryQuery(string accountNumber)
            : this(DateTime.MinValue, DateTime.MaxValue, accountNumber)
        {
        }

        public string AccountNumber { get; }

        public DateTime From { get; }

        public DateTime To { get; }
    }
}