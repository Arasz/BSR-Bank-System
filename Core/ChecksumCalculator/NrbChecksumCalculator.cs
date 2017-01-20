using System.Globalization;

namespace Core.Common.ChecksumCalculator
{
    public class NrbChecksumCalculator : IAccountChecksumCalculator
    {
        /// <summary>
        /// PL (A=10,...,Z=35) 
        /// </summary>
        private readonly string _countryCodeNumber;

        public NrbChecksumCalculator()
        {
            _countryCodeNumber = "2521";
        }

        public int Calculate(string slimAccountNumber)
        {
            // Add dummy checksum and parsed country code
            var accountNumber = _countryCodeNumber + "00" + slimAccountNumber;

            var movedPartLength = 6;

            // Move country code and checksum at the end
            accountNumber = accountNumber.Substring(movedPartLength, accountNumber.Length - movedPartLength) +
                            accountNumber.Substring(0, movedPartLength);

            var firstPart = accountNumber.Substring(0, accountNumber.Length / 2);
            var secondPart = accountNumber.Substring(accountNumber.Length / 2, accountNumber.Length / 2);

            // Parse to number (with method for very long numbers)
            var parsedFirstPart = ulong.Parse(firstPart, NumberStyles.Integer);

            var firstModulo = parsedFirstPart % 97;

            var parsedSecondPartCombined = ulong.Parse(firstModulo + secondPart, NumberStyles.Integer);

            var checksum = 98 - parsedSecondPartCombined % 97;

            return (int)checksum;
        }

        public bool IsCorrect(string accountNumber)
        {
            var checksum = accountNumber.Substring(0, 2);
            var slimAccountNumber = accountNumber.Substring(2, accountNumber.Length - 2);

            return Calculate(slimAccountNumber) == int.Parse(checksum, NumberStyles.Integer);
        }
    }
}