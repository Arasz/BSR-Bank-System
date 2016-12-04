using System;

namespace AccountMangmentService.History
{
    /// <summary>
    /// Operations history query 
    /// </summary>
    public class HistoryQuery
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}