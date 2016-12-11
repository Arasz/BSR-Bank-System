using CQRS.Commands;
using System;

namespace CQRS.Validation.Decorators
{
    /// <summary>
    /// Command bus decorator. Validates command before send. 
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly ICommandBus _commandBus;
        private readonly Func<Type, IValidation> _validationFactory;

        public CommandBus(ICommandBus commandBus, Func<Type, IValidation> validationFactory)
        {
            _commandBus = commandBus;
            _validationFactory = validationFactory;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var validator = _validationFactory(typeof(TCommand)) as IValidation<TCommand>;

            validator?.Validate(command);

            _commandBus.Send(command);
        }
    }
}