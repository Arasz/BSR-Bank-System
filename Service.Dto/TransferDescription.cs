using System;
using System.Runtime.Serialization;

namespace Service.Dto
{
    /// <summary>
    ///     Transfer data type object
    /// </summary>
    [DataContract]
    public class TransferDescription
    {
        public TransferDescription()
        {
        }

        public TransferDescription(string sourceAccountNumber, string targetAccountNumber, string title, decimal amount)
        {
            SourceAccountNumber = sourceAccountNumber;
            TargetAccountNumber = targetAccountNumber;
            Title = title;
            Amount = amount;
        }

        public TransferDescription(TransferDescription description)
        {
            SourceAccountNumber = description.SourceAccountNumber;
            TargetAccountNumber = description.TargetAccountNumber;
            Amount = description.Amount;
            Title = description.Title;
        }

        /// <summary>
        ///     Amount of money
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Sender account number
        /// </summary>
        [DataMember]
        public string SourceAccountNumber { get; set; }

        /// <summary>
        ///     Transfer title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        ///     Receiver account number
        /// </summary>
        [DataMember]
        public string TargetAccountNumber { get; set; }

        public static explicit operator TransferDescription(InterbankTransferDescription interbankTransferDescription)
        {
            return new TransferDescription
            {
                Amount = Convert.ToDecimal(interbankTransferDescription.Amount) / 100,
                TargetAccountNumber = interbankTransferDescription.ReceiverAccount,
                SourceAccountNumber = interbankTransferDescription.SenderAccount,
                Title = interbankTransferDescription.Title
            };
        }
    }
}