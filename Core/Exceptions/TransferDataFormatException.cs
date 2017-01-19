using System;

namespace Core.Common.Exceptions
{
    //[DataContract]
    public class TransferDataFormatException : Exception
    {
        public override string Message => $"Invalid property: {PropertyName}\n" +
                                                  $"Validation message: {ValidationMessage}\n" +
                                                  $"Message: {base.Message}";

        //[DataMember]
        public string PropertyName { get; }

        //[DataMember]
        public string ValidationMessage { get; }

        public TransferDataFormatException(string propertyName, string validationMessage, string message) : base(message)
        {
            PropertyName = propertyName;
            ValidationMessage = validationMessage;
        }
    }
}