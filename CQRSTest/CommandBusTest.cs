using CQRS.Commands;
using CQRSTest.Commands;
using FluentAssertions;
using System;
using Xunit;

namespace CQRSTest
{
    public class CommandBusTest
    {
        [Fact]
        public void SendCommand_AddArgumentsCommandResult_ShouldBeEqualToSumOfArguments()
        {
            var command = new AddNumbersCommand(2, 2);
            var commandHandler = new AddNumbersCommandHandler();

            var commandfactory = new Func<Type, AddNumbersCommandHandler>(type => commandHandler);

            var bus = new CommandBus(commandfactory);

            bus.Send(command);

            commandHandler.Result
                .Should()
                .Be(command.A + command.B, $"should be equal to sum of {nameof(AddNumbersCommand)} properties");
        }
    }
}