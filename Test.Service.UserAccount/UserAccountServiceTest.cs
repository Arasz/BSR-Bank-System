﻿using Autofac;
using Core.Common.Security;
using Core.CQRS.Commands.Autofac;
using Core.CQRS.Queries.Autofac;
using Data.Core;
using Data.Core.Entities;
using FluentAssertions;
using Moq;
using Service.UserAccount.Contract;
using Service.UserAccount.Implementation;
using Test.Common;
using Xunit;

namespace Test.Service.UserAccount
{
    public class UserAccountServiceTest : DataContextAccessTest<User>, IDependencyInjectionTest
    {
        public IContainer Container { get; set; }

        public UserAccountServiceTest()
        {
            Container = BuildContainer();
        }

        public IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterModules(containerBuilder);

            RegisterComponents(containerBuilder);

            return containerBuilder.Build();
        }

        [Fact]
        public void CreateUserAccount_PasswordAndUserNameIsCorrect_ShouldCreateUserName()
        {
            var userAccountService = Container.Resolve<IUserAccountService>();

            var userName = "Andrzej";
            var password = "Bob1234";
            var hashedPassword = "HashedPassword";

            var user = userAccountService.CreateUserAccount(userName, password);

            user.Name.ShouldAllBeEquivalentTo(userName);

            user.Password.ShouldAllBeEquivalentTo(hashedPassword);
        }

        private IPasswordHasher MockPasswordHasher()
        {
            var hasherMock = new Mock<IPasswordHasher>();

            hasherMock
                .Setup(hasher => hasher.HashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            return hasherMock.Object;
        }

        private void RegisterComponents(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .Register(componentContext => MockDataContext(context => context.Users));

            containerBuilder
                .Register(context => MockPasswordHasher())
                .AsImplementedInterfaces();

            containerBuilder
                .RegisterType<UserAccountService>()
                .AsImplementedInterfaces();
        }

        private void RegisterModules(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new CommandModule(typeof(UserAccountService).Assembly));
            containerBuilder.RegisterModule(new QueryModule(typeof(UserAccountService).Assembly));
        }
    }
}