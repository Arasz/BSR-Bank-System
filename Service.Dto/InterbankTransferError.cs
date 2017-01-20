using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    public class InterbankTransferError
    {
        public InterbankTransferError(string error)
        {
            Error = error;
        }

        [DataMember]
        public string Error { get; set; }
    }
}