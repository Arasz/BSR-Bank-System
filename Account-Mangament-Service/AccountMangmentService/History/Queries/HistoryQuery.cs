using System;
using System.Collections.Generic;
using AccountMangmentService.Operations;
using CQRS.Queries;

namespace AccountMangmentService.History.Queries
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