using System;
using Core.Common.ChecksumCalculator;
using FluentValidation;
using Service.Dto;

namespace Service.Bank.Validation
{
    public class TransferDescriptionValidator : AbstractValidator<TransferDescription>
    {
        private readonly IAccountChecksumCalculator _checksumCalculator;

        public TransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator)
        {
            _checksumCalculator = checksumCalculator;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(description => description.Amount)
                .GreaterThanOrEqualTo(0);

            RuleFor(description => description.Title)
                .Length(0, 200);

            //regex https://regex101.com/r/LEt65g/1
            RuleFor(description => description.From)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .Must(HaveCorrectChecksum);

            RuleFor(description => description.To)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .Must(HaveCorrectChecksum);
        }

        private bool HaveCorrectChecksum(string accountNumber) => _checksumCalculator.IsCorrect(accountNumber);
    }
}