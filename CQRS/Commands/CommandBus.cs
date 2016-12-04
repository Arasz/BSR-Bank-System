using System;

namespace CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, ICommandHandler> _commandHandlersFactory;

        public CommandBus(Func<Type, ICommandHandler> commandHandlersFactory)
        {
            _commandHandlersFactory = commandHandlersFactory;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentException("Command can not be null");

            var commandHandler = (ICommandHandler<TCommand>)_commandHandlersFactory(typeof(TCommand));
            commandHandler.HandleCommand(command);
        }
    }
}