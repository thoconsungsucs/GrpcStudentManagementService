using GrpcStudentManagementService.DTOs;
using GrpcStudentManagementService.Models;
using Shared;

namespace GrpcStudentManagementService.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Student? GetStudentById(int studentId);
        public List<Student> GetAllStudents();
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public void DeleteStudent(Student student);
        public Task<List<Student>> GetAllPagination(StudentFilter studentFilter);
        public Task<int> CountAsync(StudentFilter studentFilter);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task AddStudentAsync(Student student);
        public Task UpdateStudentAsync(Student student);
        public Task DeleteStudentAsync(Student student);
        public Task<List<GenderCountItem>> GetGenderCountAsync(int classId = 0);
    }
}
