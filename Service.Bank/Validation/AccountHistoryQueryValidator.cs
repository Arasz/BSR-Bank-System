using Core.Common.ChecksumCalculator;
using FluentValidation;
using Service.Dto;
using System;

namespace Service.Bank.Validation
{
    public class AccountHistoryQueryValidator : AbstractValidator<AccountHistoryQuery>
    {
        private readonly IAccountChecksumCalculator _accountChecksumCalculator;

        public AccountHistoryQueryValidator(IAccountChecksumCalculator accountChecksumCalculator)
        {
            _accountChecksumCalculator = accountChecksumCalculator;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(query => query.From)
                .LessThanOrEqualTo(DateTime.Now)
                .LessThanOrEqualTo(query => query.To);

            RuleFor(query => query.AccountNumber)
                .NotEmpty()
                .Length(26)
                .Matches(@"^[0-9]{2}[0-9]{8}[0-9]{16}")
                .Must(HaveCorrectChecksum);
        }

        private bool HaveCorrectChecksum(string accountNumber) => _accountChecksumCalculator.IsCorrect(accountNumber);
    }
}