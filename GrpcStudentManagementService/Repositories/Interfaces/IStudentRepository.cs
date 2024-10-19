using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Student? GetStudentById(int studentId);
        public List<Student> GetAllStudents();
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public void DeleteStudent(Student student);
    }
}
