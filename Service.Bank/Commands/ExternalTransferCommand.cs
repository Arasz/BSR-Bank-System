﻿using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    ///     Payment to the account in other bank
    /// </summary>
    public class ExternalTransferCommand : TransferCommand
    {
        public ExternalTransferCommand(TransferDescription description) : base(description)
        {
        }
    }
}