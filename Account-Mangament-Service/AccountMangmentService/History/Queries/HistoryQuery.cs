using System;
using System.Collections.Generic;
using BankService.Operations;
using CQRS.Queries;

namespace BankService.History.Queries
{
    /// <summary>
    /// Operations history query 
    /// </summary>
    public class HistoryQuery : IQuery<IEnumerable<Operation>>
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}