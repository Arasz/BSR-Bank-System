using System;

namespace Service.Dto
{
    public class AccountHistoryQuery
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