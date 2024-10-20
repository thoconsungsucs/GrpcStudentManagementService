using System.Runtime.Serialization;

namespace Shared
{
    [DataContract]
    public class RequestId
    {
        [DataMember(Order = 1)]
        public int Value { get; set; }
    }
}
