using FluentValidation;
using InterbankTransactionService.DataStructures;
using Shared.ChecksumCalculator;
using System;

namespace InterbankTransactionService.Validation
{
    public class InterbankTransferDescriptionValidator : AbstractValidator<InterbankTransferDescription>
    {
        private readonly IAccountChecksumCalculator _checksumCalculator;

        public InterbankTransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator)
        {
            _checksumCalculator = checksumCalculator;
            RuleFor(description => description.Amount).GreaterThan(0);
            RuleFor(description => description.ReceiverAccount)
                .NotEmpty()
                .Length(26)
                .Must(HaveCorrectChecksum)
                .Must(BeValidAccountNumber);
        }

        private bool BeValidAccountNumber(string s)
        {
            throw new NotImplementedException();
        }

        private bool HaveCorrectChecksum(string accountNumber)
        {
            var checksumLength = 2;

            var checksum = int.Parse(accountNumber.Substring(0, checksumLength));

            return checksum == _checksumCalculator.Calculate(accountNumber.Substring(checksumLength, accountNumber.Length - checksumLength));
        }
    }
}