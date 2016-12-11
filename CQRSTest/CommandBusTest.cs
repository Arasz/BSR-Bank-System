using CQRS.Commands;
using CQRS.Exceptions;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace CQRSTest
{
    public class CommandBusTest
    {
        [Fact]
        public void SendCommand_AddArgumentsCommandResult_ShouldBeEqualToSumOfArguments()
        {
            var commandMock = new Mock<ICommand>();
            var commandHandlerMock = new Mock<ICommandHandler<ICommand>>();

            var result = 0;
            var expectedResult = 4;

            commandHandlerMock
                .Setup(handler => handler.HandleCommand(commandMock.Object))
                .Callback(() => result = expectedResult);

            var commandfactory = new Func<Type, ICommandHandler>(type => commandHandlerMock.Object);

            var bus = new CommandBus(commandfactory);

            bus.Send(commandMock.Object);

            result
                .Should()
                .Be(expectedResult, $"should be equal to expected result: {expectedResult}");
        }

        [Fact]
        public void SendCommand_BehaviorWithNullHandler_ShouldThrowNullHandlerException()
        {
            var commandMock = new Mock<ICommand>();

            var commandfactory = new Func<Type, ICommandHandler>(type => null);

            var bus = new CommandBus(commandfactory);

            Action action = () => bus.Send(commandMock.Object);
            action.ShouldThrow<NullHandlerException>();
        }
    }
}