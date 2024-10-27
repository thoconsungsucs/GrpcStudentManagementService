namespace GrpcStudentManagementService.DTOs
{
    public class GenderCountItem
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }
}
