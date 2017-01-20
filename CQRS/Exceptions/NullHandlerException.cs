using System;

namespace Core.CQRS.Exceptions
{
    public class NullHandlerException : Exception
    {
        public NullHandlerException(string message, Type handlerType) : base(message)
        {
            HandlerType = handlerType.Name;
        }

        public string HandlerType { get; set; }
    }
}