using FluentValidation;
using InterbankTransactionService.DataStructures;
using Shared.ChecksumCalculator;

namespace InterbankTransactionService.Validation
{
    public class InterbankTransferDescriptionValidator : AbstractValidator<InterbankTransferDescription>
    {
        private readonly IAccountChecksumCalculator _checksumCalculator;

        public InterbankTransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator)
        {
            _checksumCalculator = checksumCalculator;
            RuleFor(description => description.Amount)
                .GreaterThan(0);

            RuleFor(description => description.ReceiverAccount)
                .NotEmpty()
                .Length(26)
                .Matches("^[0-9]{2}112241[0]{2}[0-9]{18}")
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