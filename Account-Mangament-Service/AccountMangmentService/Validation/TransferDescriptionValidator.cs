using System;
using Core.Common.ChecksumCalculator;
using FluentValidation;
using Service.Dto;

namespace Service.Bank.Validation
{
    public class TransferDescriptionValidator : AbstractValidator<TransferDescription>
    {
        private readonly Func<string, bool> _accountExistPredicate;
        private readonly IAccountChecksumCalculator _checksumCalculator;

        ///TODO: Command side validation
        public TransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator, Func<string, bool> accountExistPredicate)
        {
            _checksumCalculator = checksumCalculator;
            _accountExistPredicate = accountExistPredicate;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(description => description.Amount)
                .GreaterThanOrEqualTo(0);

            //regex https://regex101.com/r/LEt65g/1
            RuleFor(description => description.From)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .Must(HaveCorrectChecksum)
                .Must(ExistInTheBank)
                .When(description => description.From.Substring(2, 6).Equals("112241"), ApplyConditionTo.CurrentValidator)
                .WithErrorCode("404 - Receiver account not found");

            RuleFor(description => description.To)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .Must(HaveCorrectChecksum)
                .Must(ExistInTheBank)
                .When(description => description.To.Substring(2, 6).Equals("112241"), ApplyConditionTo.CurrentValidator)
                .WithErrorCode("404 - Sender account not found");
        }

        private bool ExistInTheBank(string accountNumber) => _accountExistPredicate?.Invoke(accountNumber) ?? false;

        private bool HaveCorrectChecksum(string accountNumber) => _checksumCalculator.IsCorrect(accountNumber);
    }
}