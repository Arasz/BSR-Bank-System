using Core.Common.ChecksumCalculator;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Service.Bank.Validation;
using Xunit;

namespace Test.Service.Bank.Validation
{
    public class TransferDescriptionValidatorTest
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
            .ShouldHaveValidationErrorFor(description => description.TargetAccountNumber, "2323")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_CorrectAccountNumber_ShouldPassValidation() => CreateValidator()
            .ShouldNotHaveValidationErrorFor(description => description.TargetAccountNumber, "78112241008528164913108077");

        [Fact]
        public void ReceiverAccount_EmptySenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.TargetAccountNumber, "")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_IncorrectAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.TargetAccountNumber, "78e45541668528164913108077")
            .Should()
            .ContainSingle();

        [Fact]
        public void ReceiverAccount_IncorrectAccountNumberChecksum_ShouldReturnValidationErorr()
            => CreateValidator(false)
                .ShouldHaveValidationErrorFor(description => description.TargetAccountNumber, "232323223232322323232232323223232323")
                .Should()
                .ContainSingle();

        [Fact]
        public void ReceiverAccount_NullSenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.TargetAccountNumber, null as string)
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_AccountNumberShorterThan26Numbers_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SourceAccountNumber, "2323")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_CorrectAccountNumber_ShouldPassValidation() => CreateValidator()
            .ShouldNotHaveValidationErrorFor(description => description.SourceAccountNumber, "78345541008528164913108077");

        [Fact]
        public void SenderAccount_EmptySenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SourceAccountNumber, "")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_IncorrectAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SourceAccountNumber, "232323223232322323232232323223232323")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_IncorrectAccountNumberChecksum_ShouldReturnValidationErorr() => CreateValidator(false)
            .ShouldHaveValidationErrorFor(description => description.SourceAccountNumber, "232323223232322323232232323223232323")
            .Should()
            .ContainSingle();

        [Fact]
        public void SenderAccount_NullSenderAccountNumber_ShouldReturnValidationErorr() => CreateValidator()
            .ShouldHaveValidationErrorFor(description => description.SourceAccountNumber, null as string)
            .Should()
            .ContainSingle();

        private TransferDescriptionValidator CreateValidator(bool withChecksumAlwaysTrue = true,
            bool accountExist = true)
        {
            var checksumCalculator = Mock.Of<IAccountChecksumCalculator>(calculator => calculator.IsCorrect(It.IsAny<string>()) == withChecksumAlwaysTrue);

            return new TransferDescriptionValidator(checksumCalculator);
        }
    }
}