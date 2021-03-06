﻿using System;
using System.Runtime.Serialization;

namespace Service.Dto
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
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Receiver account number 
        /// </summary>
        [DataMember(Name = "receiver_account")]
        public string ReceiverAccount { get; set; }

        /// <summary>
        /// Sender account number 
        /// </summary>
        [DataMember(Name = "sender_account")]
        public string SenderAccount { get; set; }

        /// <summary>
        /// Transfer title 
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        public static explicit operator InterbankTransferDescription(TransferDescription transferDescription)
        {
            return new InterbankTransferDescription
            {
                Amount = Convert.ToInt32(Math.Round(transferDescription.Amount, 2) * 100),
                ReceiverAccount = transferDescription.ReceiverAccountNumber,
                SenderAccount = transferDescription.SenderAccountNumber,
                Title = transferDescription.Title
            };
        }
    }
}