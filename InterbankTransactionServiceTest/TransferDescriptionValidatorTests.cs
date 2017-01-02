using FluentAssertions;
using FluentValidation.TestHelper;
using InterbankTransactionService.Validation;
using Moq;
using Shared.ChecksumCalculator;
using Xunit;

namespace InterbankTransactionServiceTest
{
    public class InterbankTransferDescriptionValidatorTest
    {
        [Fact]
        public void AmountValidation_NegativeAmount_ShouldPassValidation() => CreateValidator()
            .ShouldNotHaveValidationErrorFor(description => description.Amount, 100);

        [Fact]
        public void AmountValidation_NegativeAmount_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.Amount, -1)
            .Should()
            .ContainSingle();

        [Fact]
        public void AmountValidation_ZeroAmount_ShouldPassValidation() => CreateValidator()
            .ShouldNotHaveValidationErrorFor(description => description.Amount, 0);

        [Fact]
        public void ReceiverAccount_AccountNumberShorterThan26Numbers_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.ReceiverAccount, "2323")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_CorrectAccountNumber_ShouldPassValidation() => CreateValidator()
            .ShouldNotHaveValidationErrorFor(description => description.ReceiverAccount, "78112241008528164913108077");

        [Fact]
        public void ReceiverAccount_EmptySenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.ReceiverAccount, "")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_IncorrectAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.ReceiverAccount, "78345541008528164913108077")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_IncorrectAccountNumberChecksum_ShouldReturnValidationErorr() => CreateValidator(false)
            .ShouldHaveValidationErrorFor(description => description.ReceiverAccount, "232323223232322323232232323223232323")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_NullSenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.ReceiverAccount, null as string)
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_AccountNumberShorterThan26Numbers_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SenderAccount, "2323")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_CorrectAccountNumber_ShouldPassValidation() => CreateValidator()
            .ShouldNotHaveValidationErrorFor(description => description.SenderAccount, "78345541008528164913108077");

        [Fact]
        public void SenderAccount_EmptySenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SenderAccount, "")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_IncorrectAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SenderAccount, "232323223232322323232232323223232323")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_IncorrectAccountNumberChecksum_ShouldReturnValidationErorr() => CreateValidator(false)
            .ShouldHaveValidationErrorFor(description => description.SenderAccount, "232323223232322323232232323223232323")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_NullSenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SenderAccount, null as string)
            .Should()
            .ContainSingle();

        private InterbankTransferDescriptionValidator CreateValidator(bool withChecksumAlwaysTrue = true)
        {
            var checksumCalculator = Mock.Of<IAccountChecksumCalculator>(calculator => calculator.IsCorrect(It.IsAny<string>()) == withChecksumAlwaysTrue);

            return new InterbankTransferDescriptionValidator(checksumCalculator);
        }
    }
}