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

            RuleFor(description => description.Title)
                .NotNull()
                .Length(0, 200);

            RuleFor(description => description.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0.01M);

            RuleFor(description => description.Title)
                .Length(0, 200);

            //regex https://regex101.com/r/LEt65g/1
            RuleFor(description => description.SourceAccountNumber)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .WithMessage("Wrong source account number format")
                .Must(HaveCorrectChecksum)
                .WithMessage("Wrong source account number checksum");

            RuleFor(description => description.TargetAccountNumber)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .WithMessage("Wrong target account number format")
                .Must(HaveCorrectChecksum)
                .WithMessage("Wrong target account number checksum");
        }

        private bool HaveCorrectChecksum(string accountNumber) => _checksumCalculator.IsCorrect(accountNumber);
    }
}