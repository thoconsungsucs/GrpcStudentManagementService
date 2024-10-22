using System.Runtime.Serialization;

namespace Shared
{
    [DataContract]
    public class RequestId
    {
        [DataMember(Order = 1)]
        public int Value { get; set; }
    }

    [DataContract]
    public class ListInfo<T>
    {
        [DataMember(Order = 1)]
        public List<T>? List { get; set; }
        [DataMember(Order = 2)]
        public int Total { get; set; }
    }

    [DataContract]
    public class PaginationRequest
    {
        [DataMember(Order = 1)]
        public int PageIndex { get; set; }
        [DataMember(Order = 2)]
        public int PageSize { get; set; }
    }
}
