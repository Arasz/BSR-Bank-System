﻿namespace Service.Bank.Proxies.Configuration
{
    /// <summary>
    /// Register with transfer service address for each known bank id. 
    /// </summary>
    public interface ITransferServicesRegister
    {
        string Password { get; }

        string Login { get; }

        string GetTransferServiceAddress(string bankId);
    }
}