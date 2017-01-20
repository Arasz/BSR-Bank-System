using System;

namespace Core.Common.Exceptions
{
    public class TransferDescriptionException : Exception
    {
        public override string Message => $"Invalid property: {PropertyName}." +
                                         $" Validation message: {ValidationMessage}";

        public string PropertyName { get; }

        public string ValidationMessage { get; }

        public TransferDescriptionException(string propertyName, string validationMessage)
        {
            PropertyName = propertyName;
            ValidationMessage = validationMessage;
        }
    }
}