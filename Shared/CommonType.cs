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

    [DataContract]
    public class PieChartItem
    {
        [DataMember(Order = 1)]
        public string type { get; set; }
        [DataMember(Order = 2)]
        public int value { get; set; }
    }
    [DataContract]    
    
    public class StudentCategorizeOption
    {
        [DataMember(Order = 1)]
        public string LevelName { get; set; }
        [DataMember(Order = 2)]
        public int? GradeId { get; set; } = 0;
        [DataMember(Order = 3)]
        public bool ByGrade { get; set; }
        [DataMember(Order = 4)]
        public bool ByClass { get; set; }
    }

    [DataContract]
    public class SelectionItem
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string Name { get; set; }
    }

    public static class CommonType
    {
        public static readonly List<string> LastGrades = new List<string> { "Grade 5", "Grade 9" };
    }

    public class NameAndCount
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
