using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Bank.Proxies.Configuration
{
    [DataContract]
    public class InterbankTransferConfiguration
    {
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public Dictionary<string, string> RegisteredServices { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}