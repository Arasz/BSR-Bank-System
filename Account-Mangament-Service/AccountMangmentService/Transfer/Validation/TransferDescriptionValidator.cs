using AccountMangmentService.Transfer.Dto;
using FluentValidation;
using Shared.ChecksumCalculator;

namespace AccountMangmentService.Transfer.Validation
{
    public class TransferDescriptionValidator : AbstractValidator<TransferDescription>
    {
        private readonly IAccountChecksumCalculator _checksumCalculator;

        public TransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator)
        {
            _checksumCalculator = checksumCalculator;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(description => description.Amount)
                .GreaterThan(0);
            //regex https://regex101.com/r/qnQGLK/1
            RuleFor(description => description.ReceiverAccount)
                .NotEmpty()
                .Length(26)
                .Matches("^[0-9]{2}112241[0]{2}[0-9]{18}")
                .WithErrorCode("22")
                .Must(HaveCorrectChecksum)
                .Must(ExistInTheBank);

            RuleFor(description => description.SenderAccount)
                .NotEmpty()
                .Length(26)
                .Matches("^[0-9]{2}[0-9]{4}[0]{2}[0-9]{18}")
                .Must(HaveCorrectChecksum);
        }

        private bool ExistInTheBank(string accountNumber) => true;

        private bool HaveCorrectChecksum(string accountNumber)
        {
            var checksumLength = 2;

            var checksum = int.Parse(accountNumber.Substring(0, checksumLength));

            return checksum == _checksumCalculator.Calculate(accountNumber.Substring(checksumLength, accountNumber.Length - checksumLength));
        }
    }
}