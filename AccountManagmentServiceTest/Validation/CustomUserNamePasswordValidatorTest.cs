using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Data.Core;
using FluentAssertions;
using Service.Bank.Validation;
using Shared.Security;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.Validation
{
    public class CustomUserNamePasswordValidatorTest : DataContextAccessTest<User>
    {
        private BankDataContext _dataContextMock;

        private string _hashedPassword = "3MmtbNTbJeSmWngoY7l6gsqB1pVCegxCtBdUTUIDXaj2R6ac";
        private IPasswordHasher _passwordHasher;
        private string _unhashedPassword = "DogesEqualityAndHonor";
        private string _userName = "Test";

        private CustomUserNamePasswordValidator NewValidator => new CustomUserNamePasswordValidator(_dataContextMock, _passwordHasher);

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
        public void ValidateUser_EmptyUsernameEndPassword_ShouldThrowSecurityException(string username, string password)
        {
            CreateUser();

            var validator = NewValidator;

            Action validationAction = () => validator.Validate("", _unhashedPassword);

            validationAction.ShouldThrow<SecurityTokenException>();
        }

        [Fact]
        public void ValidateUser_WrongPassword_ShouldThrowSecurityException()
        {
            CreateUser();

            var validator = NewValidator;

            Action validationAction = () => validator.Validate(_userName, "WrongPassword");

            validationAction.ShouldThrow<SecurityTokenException>();
        }

        [Fact]
        public void ValidateUser_WrongUsername_ShouldThrowSecurityException()
        {
            CreateUser();

            var validator = NewValidator;

            Action validationAction = () => validator.Validate("WrongUsername", _unhashedPassword);

            validationAction.ShouldThrow<SecurityTokenException>();
        }

        private User CreateUser()
        {
            var user = new User
            {
                Accounts = new List<Account>(),
                Name = _userName,
                Password = _hashedPassword,
                Id = 1,
            };
            MockDataSource.Add(user);
            return user;
        }
    }
}