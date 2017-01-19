using System.Collections.Generic;
using System.IO;
using Core.Common.Configuration;
using Newtonsoft.Json;
using Service.Bank.Exceptions;

namespace Service.Bank.Proxy.ServicesRegister
{
    public class TransferServicesRegister : ITransferServicesRegister
    {
        private readonly string _registerPath;
        private InterbankTransferConfiguration _interbankTransferConfiguration;

        public string Login => InterbankTransferConfiguration.Login;

        public string Password => InterbankTransferConfiguration.Password;

        private InterbankTransferConfiguration InterbankTransferConfiguration
        {
            get
            {
                if (_interbankTransferConfiguration == null)
                    LoadConfigurationFromFile();

                return _interbankTransferConfiguration;
            }
        }

        private Dictionary<string, string> ServicesRegister => InterbankTransferConfiguration.RegisteredServices;

        public TransferServicesRegister(string registerPath)
        {
            _registerPath = registerPath;
        }

        public string GetTransferServiceAddress(string bankId)
        {
            var serviceAddres = "";
            if (TryGetTransferServiceAddress(bankId, out serviceAddres))
                return serviceAddres;

            throw new TransferServiceNotFoundException(bankId);
        }

        private void LoadConfigurationFromFile()
            => _interbankTransferConfiguration = InterbankTransferConfiguration.LoadFromFile(_registerPath);

        private bool TryGetTransferServiceAddress(string bankId, out string serviceAddress)
            => ServicesRegister.TryGetValue(bankId, out serviceAddress);
    }
}