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
        public Task<List<Student>> GetAllPagination(int pageIndex, int pageSize);
        public Task<int> CountAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task AddStudentAsync(Student student);
        public Task UpdateStudentAsync(Student student);

        public Task DeleteStudentAsync(Student student);
    }
}
