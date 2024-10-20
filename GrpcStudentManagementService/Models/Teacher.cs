namespace GrpcStudentManagementService.Models
{
    public class Teacher
    {
        public virtual int TeacherId { get; }
        public virtual string TeacherName { get; set; }
        public virtual DateTime Dob { get; set; }
    }
}
