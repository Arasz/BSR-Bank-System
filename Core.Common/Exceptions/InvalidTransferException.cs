using System;

namespace Core.Common.Exceptions
{
    public class InvalidTransferException : Exception
    {
        public InvalidTransferException(string propertyName, string validationMessage)
        {
            PropertyName = propertyName;
            ValidationMessage = validationMessage;
        }

        public override string Message => $"Invalid property: {PropertyName}." +
                                          $" Validation message: {ValidationMessage}";

        public string PropertyName { get; }

        public string ValidationMessage { get; }
    }
}