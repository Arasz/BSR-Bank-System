using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Service.Bank.Proxies.Configuration
{
    public class InterbankServiceAddressProvider
    {
        private readonly string _mapFileName = "BankIdServiceAddressMap.json";

        private Dictionary<string, string> _bankIdServiceAddressMap;

        public InterbankServiceAddressProvider()
        {
            ReadServiceAddressMap();
        }

        public string GetBankServiceAddress(string bankId) => _bankIdServiceAddressMap[bankId];

        public bool HasBankServiceAddress(string bankId) => _bankIdServiceAddressMap.ContainsKey(bankId);

        public bool TryGetBankServiceAddress(string bankId, out string bankAddress)
        {
            var hasAddress = HasBankServiceAddress(bankId);

            bankAddress = hasAddress ? GetBankServiceAddress(bankId) : "";

            return hasAddress;
        }

        private void ReadServiceAddressMap()
        {
            var jsonSerializer = new JsonSerializer();

            using (var stream = File.OpenRead(_mapFileName))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                _bankIdServiceAddressMap = jsonSerializer.Deserialize<Dictionary<string, string>>(jsonReader);
            }
        }
    }
}