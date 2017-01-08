using System.Net.Http;
using Service.Bank.Proxies.Configuration;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.Proxies
{
    /// <summary>
    /// Proxy for conntact with other 
    /// </summary>
    public class InterbankTransactionServiceProxy : IInterbankTransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly InterbankServiceAddressProvider _interbankServiceAddressProvider;

        public InterbankTransactionServiceProxy(HttpClient httpClient, InterbankServiceAddressProvider interbankServiceAddressProvider)
        {
            _httpClient = httpClient;
            _interbankServiceAddressProvider = interbankServiceAddressProvider;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            throw new System.NotImplementedException();
        }
    }
}