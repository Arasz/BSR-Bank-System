using System;
using System.Runtime.Serialization;

namespace Core.Common.Exceptions
{
    [DataContract]
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException(string message) : base(message)
        {
        }
    }
}