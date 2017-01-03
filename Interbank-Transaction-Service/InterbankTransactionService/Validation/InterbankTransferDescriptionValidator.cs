﻿using FluentValidation;
using Service.InterbankTransaction.Dto;
using Shared.ChecksumCalculator;

namespace Service.InterbankTransaction.Validation
{
    public class InterbankTransferDescriptionValidator : AbstractValidator<InterbankTransferDescription>
    {
        private readonly IAccountChecksumCalculator _checksumCalculator;

        public InterbankTransferDescriptionValidator(IAccountChecksumCalculator checksumCalculator)
        {
            _checksumCalculator = checksumCalculator;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(description => description.Amount)
                .GreaterThanOrEqualTo(0);

            //regex https://regex101.com/r/LEt65g/1
            RuleFor(description => description.ReceiverAccount)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}112241[0]{2}[0-9]{16}")
                .Must(HaveCorrectChecksum);

            RuleFor(description => description.SenderAccount)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{6}[0]{2}[0-9]{16}")
                .Must(HaveCorrectChecksum);
        }

        private bool HaveCorrectChecksum(string accountNumber) => _checksumCalculator.IsCorrect(accountNumber);
    }
}