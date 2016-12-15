using System.Runtime.Serialization;

namespace InterbankTransactionService.DataStructures
{
    /// <summary>
    /// Data for interbank transfer operation 
    /// </summary>
    [DataContract]
    public class InterbankTransferDescription
    {
        /// <summary>
        /// Transfer amount as lowest money unit 
        /// </summary>
        [DataMember]
        public int Amount { get; set; }

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