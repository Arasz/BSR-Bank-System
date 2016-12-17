using System;

namespace AccountMangmentService.Operations
{
    /// <summary>
    /// Operation executed in bank system 
    /// </summary>
    public class Operation
    {
        private string AccountNumber { get; set; }

        private decimal Amount { get; set; }

        /// <summary>
        /// Balance after operation 
        /// </summary>
        private decimal Balance { get; set; }

        private string Title { get; set; }

        /// <summary>
        /// Operation type 
        /// </summary>
        private Type Type { get; set; }
    }
}