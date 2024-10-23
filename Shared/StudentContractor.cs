using GrpcStudentManagementService.Exceptions;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Shared
{
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        public Result<StudentShared> GetStudentById(RequestId request);
        [OperationContract]
        public Result<List<StudentShared>> GetAllStudents();
        [OperationContract]
        public Result AddStudent(StudentShared student);
        [OperationContract]
        public Result UpdateStudent(StudentShared student);
        [OperationContract]
        public Result DeleteStudent(RequestId request);
        [OperationContract]
        public Task<Result<ListInfo<StudentShared>>> GetAllPaginationAsync(PaginationRequest request);
        [OperationContract]
        public Task<Result> AddAsync(StudentShared student);
        [OperationContract]
        public Task<Result> UpdateAsync(StudentShared student);
        [OperationContract]
        public Task<Result> DeleteAsync(RequestId request);
    }

    [DataContract]
    public class StudentRequestId
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }

    [DataContract]
    public class StudentShared
    {
        [DataMember(Order = 1)]
        public int StudentId { get; set; }

        [DataMember(Order = 2)]
        public string StudentName { get; set; }

        [DataMember(Order = 3)]
        public DateTime Dob { get; set; }

        [DataMember(Order = 4)]
        public string Address { get; set; }

        [DataMember(Order = 5)]
        public int ClassId { get; set; }

        [DataMember(Order = 6)]
        public string ClassName { get; set; }

        [DataMember(Order = 7)]
        public string Subject { get; set; }
    }


}
