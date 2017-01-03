using System;
using System.Collections.Generic;
using CQRS.Queries;
using Service.Bank.Operations;

namespace Service.Bank.History.Queries
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