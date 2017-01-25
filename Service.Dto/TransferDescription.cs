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

        public TransferDescription(string senderAccountNumber, string receiverAccountNumber, string title, decimal amount)
        {
            SenderAccountNumber = senderAccountNumber;
            ReceiverAccountNumber = receiverAccountNumber;
            Title = title;
            Amount = amount;
        }

        public TransferDescription(TransferDescription description)
        {
            SenderAccountNumber = description.SenderAccountNumber;
            ReceiverAccountNumber = description.ReceiverAccountNumber;
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
        public string SenderAccountNumber { get; set; }

        /// <summary>
        ///     Transfer title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        ///     Receiver account number
        /// </summary>
        [DataMember]
        public string ReceiverAccountNumber { get; set; }

        public static explicit operator TransferDescription(InterbankTransferDescription interbankTransferDescription)
        {
            return new TransferDescription
            {
                Amount = Convert.ToDecimal(interbankTransferDescription.Amount) / 100,
                ReceiverAccountNumber = interbankTransferDescription.ReceiverAccount,
                SenderAccountNumber = interbankTransferDescription.SenderAccount,
                Title = interbankTransferDescription.Title
            };
        }
    }
}