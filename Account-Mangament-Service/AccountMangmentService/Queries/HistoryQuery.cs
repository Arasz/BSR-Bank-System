using AccountMangmentService.Operations;
using CQRS.Queries;
using System;
using System.Collections.Generic;

namespace AccountMangmentService.Queries
{
    /// <summary>
    /// Operations history query 
    /// </summary>
    public class HistoryQuery : IQuery<IEnumerable<IOperation>>
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}