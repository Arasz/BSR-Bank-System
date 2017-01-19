using System;
using System.Runtime.Serialization;

namespace Service.Dto
{
    /// <summary>
    /// Transfer data type object 
    /// </summary>
    [DataContract]
    public class TransferDescription
    {
        /// <summary>
        /// Amount of money 
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        /// Sender account number 
        /// </summary>
        [DataMember]
        public string From { get; set; }

        /// <summary>
        /// Transfer title 
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Receiver account number 
        /// </summary>
        [DataMember]
        public string To { get; set; }

        public TransferDescription()
        {
        }

        public TransferDescription(string from, string to, string title, decimal amount)
        {
            From = from;
            To = to;
            Title = title;
            Amount = amount;
        }

        public TransferDescription(TransferDescription description)
        {
            From = description.From;
            To = description.To;
            Amount = description.Amount;
            Title = description.Title;
        }

        public static explicit operator TransferDescription(InterbankTransferDescription interbankTransferDescription)
        {
            return new TransferDescription
            {
                Amount = Convert.ToDecimal(interbankTransferDescription.Amount) / 100,
                To = interbankTransferDescription.ReceiverAccount,
                From = interbankTransferDescription.SenderAccount,
                Title = interbankTransferDescription.Title,
            };
        }
    }
}