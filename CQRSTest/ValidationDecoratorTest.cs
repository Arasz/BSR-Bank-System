using CQRS.Commands;
using CQRS.Queries;
using CQRS.Validation;
using CQRS.Validation.Decorators;
using FluentAssertions;
using Moq;
using System;
using Xunit;
using ICommand = CQRS.Commands.ICommand;

namespace CQRSTest
{
    public class ValidationDecoratorTest
    {
        [Fact]
        public void ValidateCommand_NoValidationForCommandProvided_ShouldPassAndCallDecoratedBus()
        {
            var commandMock = new Mock<ICommand>();
            var value = 0;
            var expectedValue = 5;

            var commandBusMock = new Mock<ICommandBus>();
            commandBusMock
                .Setup(bus => bus.Send(commandMock.Object))
                .Callback(() => value = expectedValue);

            Func<Type, IValidation> validationFactory = type => null;

            var commandValidationBus = new CommandValidationBus(commandBusMock.Object, validationFactory);

            commandValidationBus.Send(commandMock.Object);

            value.Should().Be(expectedValue);
        }

        [Fact]
        public void ValidateCommand_ValidationOnCorrectCommand_ShouldPassAndCallDecoratedBus()
        {
            var commandMock = new Mock<ICommand>();
            var value = 0;
            var expectedValue = 5;

            var commandBusMock = new Mock<ICommandBus>();
            commandBusMock
                .Setup(bus => bus.Send(commandMock.Object))
                .Callback(() => value = expectedValue);

            var commandValidation = new Mock<IValidation<ICommand>>();

            Func<Type, IValidation> validationFactory = type => commandValidation.Object;

            var commandValidationBus = new CommandValidationBus(commandBusMock.Object, validationFactory);

            commandValidationBus.Send(commandMock.Object);

            value.Should().Be(expectedValue);
        }

        [Fact]
        public void ValidateCommand_ValidationOnNullCommand_ValidatorThrowsArgumentException()
        {
            var commandBusMock = new Mock<ICommandBus>();

            var validatorMock = new Mock<IValidation<ICommand>>();
            validatorMock
                .Setup(validation => validation.Validate(null))
                .Throws<ArgumentException>();

            Func<Type, IValidation> validationFactory = type => validatorMock.Object;

            var validationDecorator = new CommandValidationBus(commandBusMock.Object, validationFactory);

            Action throwing = () => validationDecorator.Send((ICommand)null);
            throwing.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ValidateQuery_NoValidationForQueryProvided_ShouldPassAndCallDecoratedBus()
        {
            var queryMock = new Mock<IQuery<int>>();
            var returnValue = 5;

            var queryBusMock = new Mock<IQueryBus>();
            queryBusMock
                .Setup(bus => bus.Send<int, IQuery<int>>(queryMock.Object))
                .Returns(() => returnValue);

            Func<Type, IValidation> validationFactory = type => null;

            var validationDecorator = new QueryValidationBus(queryBusMock.Object, validationFactory);

            var result = validationDecorator.Send<int, IQuery<int>>(queryMock.Object);

            result.Should().Be(returnValue);
        }

        [Fact]
        public void ValidateQuery_ValidationOnCorrectQuery_ShouldPassAndCallDecoratedBus()
        {
            var queryMock = new Mock<IQuery<int>>();
            var returnValue = 5;

            var queryBusMock = new Mock<IQueryBus>();
            queryBusMock
                .Setup(bus => bus.Send<int, IQuery<int>>(queryMock.Object))
                .Returns(() => returnValue);

            var validatorMock = new Mock<IValidation<IQuery<int>>>();

            Func<Type, IValidation> validationFactory = type => validatorMock.Object;

            var validationDecorator = new QueryValidationBus(queryBusMock.Object, validationFactory);

            var result = validationDecorator.Send<int, IQuery<int>>(queryMock.Object);

            result.Should().Be(returnValue);
        }

        [Fact]
        public void ValidateQuery_ValidationOnNullQuery_ValidatorThrowsArgumentException()
        {
            var queryBusMock = new Mock<IQueryBus>();

            var validatorMock = new Mock<IValidation<IQuery<int>>>();
            validatorMock
                .Setup(validation => validation.Validate(null))
                .Throws<ArgumentException>();

            Func<Type, IValidation> validationFactory = type => validatorMock.Object;

            var validationDecorator = new QueryValidationBus(queryBusMock.Object, validationFactory);

            Action throwing = () => validationDecorator.Send<int, IQuery<int>>(null);

            throwing.ShouldThrow<ArgumentException>();
        }
    }
}