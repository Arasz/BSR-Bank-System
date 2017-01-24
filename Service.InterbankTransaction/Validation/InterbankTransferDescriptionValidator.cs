using Core.Common.ChecksumCalculator;
using FluentValidation;
using Service.Dto;

namespace Service.InterbankTransfer.Validation
{
    public class InterbankTransferDescriptionValidator : AbstractValidator<InterbankTransferDescription>
    {
        private readonly IAccountChecksumCalculator _checksumCalculator;

        public InterbankTransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator)
        {
            _checksumCalculator = checksumCalculator;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(description => description.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(description => description.Title)
                .Length(0, 200);

            //regex https://regex101.com/r/LEt65g/1
            RuleFor(description => description.ReceiverAccount)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0]{2}112241[0-9]{16}")
                .WithMessage("Wrong receiver account number format")
                .Must(HaveCorrectChecksum)
                .WithMessage("Wrong receiver account number checksum");

            RuleFor(description => description.SenderAccount)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .WithMessage("Wrong sender account number format")
                .Must(HaveCorrectChecksum)
                .WithMessage("Wrong sender account number checksum");
        }

        private bool HaveCorrectChecksum(string accountNumber) => _checksumCalculator.IsCorrect(accountNumber);
    }
}