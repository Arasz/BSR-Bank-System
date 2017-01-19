using Data.Core;
using FluentAssertions;
using Service.Bank.Extensions;
using Xunit;

namespace Test.Service.Bank.Extensions
{
    public class CreditDebitCalculatorTest
    {
        [Theory]
        [InlineData(30, 20, "1", "1")]
        [InlineData(1020, 20, "1", "1")]
        [InlineData(2, 1, "1", "1")]
        public void CalculateCreditAndDebit_ForReceiverAccount_ShouldHavePositiveCreditAndZeroDebit(decimal oldBalance, decimal amount, string accountNumber, string sourceAccountNumber)
        {
            var operation = CreateOperation(oldBalance, amount, accountNumber, sourceAccountNumber);

            operation.CalculateCreditOrDebit();

            operation.Credit.Should()
                .Be(0);

            operation.Debit.Should()
                .BePositive().And
                .Be(amount);
        }

        [Theory]
        [InlineData(30, 20, "1", "2")]
        [InlineData(1020, 20, "1", "2")]
        [InlineData(2, 1, "1", "2")]
        public void CalculateCreditAndDebit_ForSenderAccount_ShouldHavePositiveDebitAndZeroCredit(decimal oldBalance, decimal amount, string accountNumber, string sourceAccountNumber)
        {
            var operation = CreateOperation(oldBalance, amount, accountNumber, sourceAccountNumber);

            operation.CalculateCreditOrDebit();

            operation.Credit.Should()
                .BePositive().And
                .Be(amount);

            operation.Debit.Should()
                .Be(0);
        }

        private Account CreateAccount(decimal balance, string number) => new Account
        {
            Balance = balance,
            Number = number,
        };

        private Operation CreateOperation(decimal balance, decimal amount, string accountNumber, string sourceNumber) => new Operation
        {
            Amount = amount,
            Balance = balance,
            Source = sourceNumber,
            Target = "target",
            Account = CreateAccount(balance, accountNumber)
        };
    }
}