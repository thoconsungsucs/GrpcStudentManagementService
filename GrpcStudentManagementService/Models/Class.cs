namespace GrpcStudentManagementService.Models
{
    public class Class
    {
        public virtual int ClassId { get; set; }
        public virtual required string ClassName { get; set; }
        public virtual string Subject { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Grade? Grade { get; set; }

    }
}
