using System.Runtime.Serialization;

namespace Shared.Transfer
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
        /// Receiver account number 
        /// </summary>
        [DataMember]
        public string ReceiverAccount { get; set; }

        /// <summary>
        /// Sender account number 
        /// </summary>
        [DataMember]
        public string SenderAccount { get; set; }

        /// <summary>
        /// Transfer title 
        /// </summary>
        [DataMember]
        public string Title { get; set; }
    }
}