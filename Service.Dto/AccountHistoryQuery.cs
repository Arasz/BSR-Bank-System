using System;
using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    public class AccountHistoryQuery
    {
        [DataMember]
        public string AccountNumber { get; }

        [DataMember]
        public DateTime From { get; }

        [DataMember]
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