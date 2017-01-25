using Core.CQRS.Exceptions;
using System;

namespace Core.CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, ICommandHandler> _commandHandlersFactory;

        public CommandBus(Func<Type, ICommandHandler> commandHandlersFactory)
        {
            _commandHandlersFactory = commandHandlersFactory;
        }

        /// <exception cref="NullHandlerException"> Factory method returned null command handler </exception>
        /// <exception cref="ArgumentException"> Command can not be null </exception>
        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentException("Command can not be null");

            var commandHandler = (ICommandHandler<TCommand>)_commandHandlersFactory(typeof(TCommand));

            if (commandHandler == null)
                throw new NullHandlerException("Factory method returned null command handler",
                    typeof(ICommandHandler<TCommand>));

            commandHandler.HandleCommand(command);
        }
    }
}