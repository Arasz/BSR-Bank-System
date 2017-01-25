using Data.Core.Entities;
using System;

namespace Client.LightClient.Model
{
    public class BankOperation
    {
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public BankOperation(Operation operation)
        {
            Amount = operation.Amount;
            Balance = operation.Balance;
            CreationDate = operation.CreationDate;
            Credit = operation.Credit;
            Debit = operation.Debit;
            Source = operation.Source;
            Target = operation.Target;
            Title = operation.Title;
            Type = operation.Type;
        }
    }
}