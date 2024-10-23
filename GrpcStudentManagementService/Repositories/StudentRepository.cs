using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace GrpcStudentManagementService.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ISession _session;

        public StudentRepository(ISession session)
        {
            _session = session;
        }

        public void AddStudent(Student student)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Save(student);
                transaction.Commit();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Update(student);
                transaction.Commit();
            }
        }

        public void DeleteStudent(Student student)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Delete(student);
                transaction.Commit();
            }
        }

        public List<Student> GetAllStudents()
        {
            return _session.Query<Student>().ToList();
        }

        public Student GetStudentById(int id)
        {
            return _session.Get<Student>(id);
        }
        
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _session.GetAsync<Student>(id);
        }

        public async Task<List<Student>> GetAllPagination(int pageIndex, int pageSize)
        {
            return await _session.Query<Student>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _session.Query<Student>().CountAsync();
        }

        public async Task AddStudentAsync(Student student)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                await _session.SaveAsync(student);
                transaction.Commit();
            }
        }
        
        public async Task UpdateStudentAsync(Student student)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                await _session.UpdateAsync(student);
                transaction.Commit();
            }
        }

        public async Task DeleteStudentAsync(Student student)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                await _session.DeleteAsync(student);
                transaction.Commit();
            }
        }
    }
}
