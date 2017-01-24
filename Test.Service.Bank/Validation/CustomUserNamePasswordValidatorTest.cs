using Core.Common.Security;
using Data.Core;
using FluentAssertions;
using Service.Bank.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Data.Core.Entities;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.Validation
{
    public class CustomUserNamePasswordValidatorTest : DataContextAccessTest<User>
    {
        private readonly BankDataContext _dataContextMock;

        private readonly string _hashedPassword = "3MmtbNTbJeSmWngoY7l6gsqB1pVCegxCtBdUTUIDXaj2R6ac";

        private readonly IPasswordHasher _passwordHasher;

        private readonly string _unhashedPassword = "DogesEqualityAndHonor";

        private readonly string _userName = "Test";

        private CustomUserNamePasswordValidator NewValidator
            => new CustomUserNamePasswordValidator(_dataContextMock, _passwordHasher);

        public CustomUserNamePasswordValidatorTest()
        {
            _dataContextMock = MockDataContext(context => context.Users);
            _passwordHasher = new DefaultPasswordHasher();
        }

        [Fact]
        public void ValidateUser_CorrectAuthenticationData_ShouldAuthenticate()
        {
            CreateUser();

            var validator = NewValidator;

            Action validationAction = () => validator.Validate(_userName, _unhashedPassword);

            validationAction.ShouldNotThrow();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "DogesEqualityAndHonor")]
        [InlineData("Test", "")]
        [InlineData("   ", "    ")]
        [InlineData("Test", "WrongPassword")]
        [InlineData("WrongUsername", "DogesEqualityAndHonor")]
        public void ValidateUser_WrongAuthenticationData_ShouldThrowSecurityException(string username, string password)
        {
            CreateUser();

            var validator = NewValidator;

            Action validationAction = () => validator.Validate("", _unhashedPassword);

            validationAction.ShouldThrow<InvalidCredentialException>();
        }

        private User CreateUser()
        {
            var user = new User
            {
                Accounts = new List<Account>(),
                Name = _userName,
                Password = _hashedPassword,
                Id = 1
            };
            MockDataSource.Add(user);
            return user;
        }
    }
}