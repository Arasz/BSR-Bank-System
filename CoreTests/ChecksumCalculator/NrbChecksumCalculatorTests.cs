using Core.Common.ChecksumCalculator;
using FluentAssertions;
using Xunit;

namespace SharedTests.ChecksumCalculator
{
    public class NrbChecksumCalculatorTests
    {
        /// <param name="slimAccountNumber"> Account number without checksum </param>
        /// <param name="correctChecksum"></param>
        [Theory]
        [InlineData("249093858528164913108077", 78)]
        [InlineData("109052723974493772627975", 84)]
        [InlineData("001122410000000000000001", 65)]
        [InlineData("001122410000000000000002", 38)]
        public void Calculate_CalculateChecksumFromCorrectSlimAccountNumber_ShouldReturnChecksum(
            string slimAccountNumber, int correctChecksum)
        {
            var calculator = new NrbChecksumCalculator();

            calculator.Calculate(slimAccountNumber)
                .Should()
                .Be(correctChecksum);
        }

        [Theory]
        [InlineData("78249093858528164913108077", true)]
        [InlineData("84109052723974493772627975", true)]
        [InlineData("84109052723474493772627975", false)]
        [InlineData("841090527234744", false)]
        public void Calculate_CheckIfAccountNumberCompatibilityWithNrbStandard_ReturnsTrueIfCorrect(
            string accountNumber, bool correct)
        {
            var calculator = new NrbChecksumCalculator();

            calculator.IsCorrect(accountNumber)
                .Should()
                .Be(correct);
        }
    }
}