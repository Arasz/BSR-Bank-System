using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Core.Common.Configuration
{
    [DataContract]
    public class InterbankTransferConfiguration
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public Dictionary<string, string> RegisteredServices { get; set; }

        public static InterbankTransferConfiguration LoadFromFile(string path)
        {
            var jsonSerializer = new JsonSerializer();

            using (var stream = File.OpenRead(path))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return jsonSerializer.Deserialize<InterbankTransferConfiguration>(jsonReader);
            }
        }
    }
}