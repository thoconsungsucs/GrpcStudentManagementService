namespace GrpcStudentManagementService.Models
{
    public class Teacher
    {
        public virtual int TeacherId { get; }
        public virtual string TeacherName { get; set; }
        public DateTime Dob { get; set;}
    }
}
