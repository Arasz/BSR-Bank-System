using System;

namespace CQRS.Exceptions
{
    public class NullHandlerException : Exception
    {
        public string HandlerType { get; set; }

        public NullHandlerException(string message, Type handlerType) : base(message)
        {
            HandlerType = handlerType.Name;
        }
    }
}