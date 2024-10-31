namespace GrpcStudentManagementService.Models
{
    public class Grade
    {
        public virtual int GradeId { get; set; }
        public virtual string GradeName { get; set; }
        public virtual Level Level { get; set; }
    }
}
