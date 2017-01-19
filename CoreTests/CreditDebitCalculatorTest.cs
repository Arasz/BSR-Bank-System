using Core.Common.Calculators;
using FluentAssertions;
using Xunit;

namespace SharedTests
{
    public class CreditDebitCalculatorTest
    {
        [Theory]
        [InlineData(30, 20)]
        [InlineData(1020, 20)]
        [InlineData(2, 1)]
        public void CalculateCreditAndDebit_BalanceDecreased_ShouldHavePositiveDebitAndZeroCredit(decimal oldBalance, decimal newBalance)
        {
            var calculator = new CreditDebitCalculator(oldBalance);

            calculator.CalculateCreditAndDebit(newBalance);

            calculator.Debit.Should()
                .BePositive().And
                .Be(oldBalance - newBalance);

            calculator.Credit.Should()
                .Be(0);
        }

        [Theory]
        [InlineData(10, 20)]
        [InlineData(1020, 1022)]
        [InlineData(0, 50)]
        public void CalculateCreditAndDebit_BalanceIncreased_ShouldHavePositiveCreditAndZeroDebit(decimal oldBalance,
            decimal newBalance)
        {
            var calculator = new CreditDebitCalculator(oldBalance);

            calculator.CalculateCreditAndDebit(newBalance);

            calculator.Credit.Should()
                .BePositive().And
                .Be(newBalance - oldBalance);

            calculator.Debit.Should()
                .Be(0);
        }

        [Theory]
        [InlineData(30, 30)]
        [InlineData(0, 0)]
        public void CalculateCreditAndDebit_TheSameBalance_CreditAndBalanceShouldBeZero(decimal oldBalance, decimal newBalance)
        {
            var calculator = new CreditDebitCalculator(oldBalance);

            calculator.CalculateCreditAndDebit(newBalance);

            calculator.Debit.Should()
                .Be(0);

            calculator.Credit.Should()
                .Be(0);
        }
    }
}