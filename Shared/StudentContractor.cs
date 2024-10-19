using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        public StudentShared? GetStudentById(int studentId);
        [OperationContract]
        public List<StudentShared> GetAllStudents();
        [OperationContract]
        public void AddStudent(StudentShared student);
        [OperationContract]
        public void UpdateStudent(StudentShared student);
        [OperationContract]
        public void DeleteStudent(int studentId);
    }

    public class StudentShared
    {
        [DataMember(Order = 1)]
        public int StudentId { get; set; }

        [DataMember(Order = 2)]
        public string StudentName { get; set; };

        [DataMember(Order = 3)]
        public DateTime Dob { get; set; }

        [DataMember(Order = 4)]
        public string Address { get; set; }
        
        [DataMember(Order = 5)]
        public string TeacherId { get; set; }
        
        [DataMember(Order = 6)]
        public string TeacherName { get; set; }
        
        [DataMember(Order = 7)]
        public int ClassId { get; set; }
        
        [DataMember(Order = 8)]
        public string ClassName { get; set; }
        
        [DataMember(Order = 9)]
        public string Subject { get; set; }
    }
}
