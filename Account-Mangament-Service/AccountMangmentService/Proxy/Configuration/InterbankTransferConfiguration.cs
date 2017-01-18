using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Bank.Proxy.Configuration
{
    [DataContract]
    public class InterbankTransferConfiguration
    {
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public Dictionary<string, string> RegisteredServices { get; set; }

        [DataMember]
        public string Login { get; set; }
    }
}