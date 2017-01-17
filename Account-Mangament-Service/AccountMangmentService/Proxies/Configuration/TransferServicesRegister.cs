using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Service.Bank.Proxies.Configuration
{
    public class TransferServicesRegister : ITransferServicesRegister
    {
        private readonly JsonSerializer _jsonSerializer;
        private readonly string _registerPath;
        private InterbankTransferConfiguration _interbankTransferConfiguration;

        public string Password => InterbankTransferConfiguration.Password;

        public string UserName => InterbankTransferConfiguration.Username;

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
            _jsonSerializer = new JsonSerializer();
        }

        public string GetTransferServiceAddress(string bankId)
        {
            var serviceAddres = "";
            if (TryGetTransferServiceAddress(bankId, out serviceAddres))
                return serviceAddres;

            throw new TransferServiceNotFoundException(bankId);
        }

        private void LoadConfigurationFromFile()
        {
            using (var stream = File.OpenRead(_registerPath))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                _interbankTransferConfiguration = _jsonSerializer.Deserialize<InterbankTransferConfiguration>(jsonReader);
            }
        }

        private bool TryGetTransferServiceAddress(string bankId, out string serviceAddress) => ServicesRegister.TryGetValue(bankId, out serviceAddress);
    }
}