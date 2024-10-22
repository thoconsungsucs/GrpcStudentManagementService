using GrpcStudentManagementService.Exceptions;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Shared
{
    [ServiceContract]
    public interface IClassService
    {
        [OperationContract]
        public Result<ClassShared> GetClassById(RequestId classId);
        [OperationContract]
        public Result<List<ClassShared>> GetAllClasses();
        [OperationContract]
        public Task<Result<List<ClassSelection>>> GetClassSelectionAsync();

    }

    [DataContract]
    public class ClassShared
    {
        [DataMember(Order = 1)]
        public int ClassId { get; set; }

        [DataMember(Order = 2)]
        public string ClassName { get; set; }

        [DataMember(Order = 3)]
        public string Subject { get; set; }

        [DataMember(Order = 4)]
        public string TeacherId { get; set; }

        [DataMember(Order = 5)]
        public string TeacherName { get; set; }
    }

    [DataContract]
    public class ClassSelection
    {
        [DataMember(Order = 1)]
        public int ClassId { get; set; }
        [DataMember(Order = 2)]
        public string ClassName { get; set; }
    }
}
