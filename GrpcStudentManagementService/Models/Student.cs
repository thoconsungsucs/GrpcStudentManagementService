using Shared;

namespace GrpcStudentManagementService.Models
{
    public class Student
    {
        public virtual int StudentId { get; }
        public virtual required string StudentName { get; set; }
        public virtual DateTime Dob { get; set; }
        public virtual required string Address { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Class? Class { get; set; }

    }

}
