using System;

namespace AccountMangmentService.Operations
{
    /// <summary>
    /// Operation executed in bank system 
    /// </summary>
    public interface IOperation
    {
        string AccountNumber { get; set; }

        decimal Amount { get; set; }

        /// <summary>
        /// Balance after operation 
        /// </summary>
        decimal Balance { get; set; }

        string Title { get; set; }

        /// <summary>
        /// Operation type 
        /// </summary>
        Type Type { get; set; }
    }
}