using Autofac;
using Core.Common.Security;
using Data.Core;
using FluentAssertions;
using Moq;
using Service.Bank.Queries;
using Service.Bank.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Security.Authentication;
using Data.Core.Entities;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.QueryHandlers
{
    public class AuthenticatedUserQueryHandlerTest : HandlerTestBase<AuthenticatedUserQueryHandler, User>
    {
        private readonly string _hashedPassword = "3MmtbNTbJeSmWngoY7l6gsqB1pVCegxCtBdUTUIDXaj2R6ac";
        private readonly string _unhashedPassword = "DogesEqualityAndHonor";
        private readonly string _userName = "Test";

        protected override Expression<Func<BankDataContext, DbSet<User>>> SelectDataSetFromDataContextExpression
            => context => context.Users;

        [Fact(Skip = "Implementation of query handle contains extension method which can not be mocked. Test always fails")]
        public void QueryForAuthenticatedUser_CorrectAuthenticationData_ShouldReturnExpectedUser()
        {
            var expectedUser = CreateUser();

            var queryHandler = Handler;

            var returnedUser = queryHandler.HandleQuery(CreateAuthenticatedUserQuery(_userName, _unhashedPassword));

            returnedUser.Name
                .Should().Be(expectedUser.Name);
            returnedUser.Password
                .Should().Be(expectedUser.Password);
            returnedUser.Accounts.Count
                .Should().Be(expectedUser.Accounts.Count);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "DogesEqualityAndHonor")]
        [InlineData("Test", "")]
        [InlineData("   ", "    ")]
        [InlineData("dsfd", "  dsf  ")]
        public void QueryForAuthenticatedUser_WrongAuthenticationData_ShouldThrowSecurityException(string username,
            string password)
        {
            CreateUser();

            var queryHandler = Handler;

            Action validationAction = () => queryHandler.HandleQuery(CreateAuthenticatedUserQuery(username, password));

            validationAction.ShouldThrow<InvalidCredentialException>();
        }

        protected override void RegisterComponents(ContainerBuilder builder)
        {
            base.RegisterComponents(builder);

            builder.RegisterType<DefaultPasswordHasher>()
                .AsImplementedInterfaces();
        }

        private AuthenticatedUserQuery CreateAuthenticatedUserQuery(string userName, string password)
            => new AuthenticatedUserQuery(userName, password);

        private User CreateUser()
        {
            var user = new User
            {
                Accounts = new List<Account> { Mock.Of<Account>(), Mock.Of<Account>() },
                Name = _userName,
                Password = _hashedPassword,
                Id = 1
            };
            MockDataSource.Add(user);
            return user;
        }
    }
}