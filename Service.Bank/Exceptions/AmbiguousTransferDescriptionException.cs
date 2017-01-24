using System;

namespace Service.Bank.Exceptions
{
    public class AmbiguousTransferDescriptionException : Exception
    {
        public string From { get; }

        public string To { get; }

        public AmbiguousTransferDescriptionException(string from, string to) : base($"Transfer description contians addresses from two external banks.\n" +
                                                                               $"Transfer target {to}\n" +
                                                                               $"Transfer source:{from}")
        {
            From = @from;
            To = to;
        }
    }
}