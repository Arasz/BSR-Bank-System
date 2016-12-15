namespace Shared.ChecksumCalculator
{
    /// <summary>
    /// Can calculate 
    /// </summary>
    public interface IAccountChecksumCalculator
    {
        /// <summary>
        /// Calculates checksum of given account number 
        /// </summary>
        /// <param name="slimAccountNumber"> Account number without country code and checksum </param>
        /// <returns> Calculated checksum </returns>
        int Calculate(string slimAccountNumber);

        /// <summary>
        /// Checks if account number is consistent with checksum 
        /// </summary>
        /// <param name="accountNumber"> Account number without country code </param>
        /// <returns> True if number is correct </returns>
        bool IsCorrect(string accountNumber);
    }
}