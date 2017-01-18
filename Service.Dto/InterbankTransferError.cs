using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    public class InterbankTransferError
    {
        [DataMember]
        public string Error { get; set; }

        public InterbankTransferError(string error)
        {
            Error = error;
        }
    }
}