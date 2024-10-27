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

    [DataContract]
    public class StudentFilter
    {
        [DataMember(Order = 1)]
        public int StudentId { get; set; }
        [DataMember(Order = 2)]
        public string StudentName { get; set; }
        [DataMember(Order = 3)]
        public string Address { get; set; }
        [DataMember(Order = 4)]
        public int ClassId { get; set; }
        [DataMember(Order = 5)]
        public DateTime? DobFrom { get; set; }
        [DataMember(Order = 6)]
        public DateTime? DobTo { get; set; }
        [DataMember(Order = 7)]
        public int PageIndex { get; set; }
        [DataMember(Order = 8)]
        public int PageSize { get; set; }
    }

    [DataContract]
    public class Filter
    {
        [DataMember(Order = 1)]
        public int StudentId { get; set; }
    }
}
