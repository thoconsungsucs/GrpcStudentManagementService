using GrpcStudentManagementService.DTOs;
using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using Shared;
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

        public IQueryable<Student> Filter(StudentFilter studentFilter)
        {
            var query = _session.Query<Student>();
            if (studentFilter != null)
            {
                if (studentFilter.ClassId != 0)
                {
                    if (studentFilter.ClassId != 0)
                    {
                        query = query.Where(s => s.Class != null && s.Class.ClassId == studentFilter.ClassId);
                    }
                }
                if (studentFilter.StudentId != 0)
                {
                    query = query.Where(s => s.StudentId == studentFilter.StudentId);
                }

                if (!string.IsNullOrEmpty(studentFilter.StudentName))
                {
                    query = query.Where(s => s.StudentName.Contains(studentFilter.StudentName));
                }

                if (!string.IsNullOrEmpty(studentFilter.Address))
                {
                    query = query.Where(s => s.Address.Contains(studentFilter.Address));
                }
                if (studentFilter.DobFrom != null)
                {
                    query = query.Where(s => s.Dob >= studentFilter.DobFrom);
                }
                if (studentFilter.DobTo != null)
                {
                    query = query.Where(s => s.Dob <= studentFilter.DobTo);
                }
            }
            return query;
        }

        public async Task<List<Student>> GetAllPagination(StudentFilter studentFilter)
        {
            var query = Filter(studentFilter);
            return await query
                .Skip((studentFilter.PageIndex - 1) * studentFilter.PageSize)
                .Take(studentFilter.PageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync(StudentFilter studentFilter)
        {
            var query = Filter(studentFilter);
            return await query.CountAsync();
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

        public async Task<List<GenderCountItem>> GetGenderCountAsync(int classId = 0)
        {
            var sqlQuery = _session.CreateSQLQuery(
                @"
                WITH GenderCounts AS (
                    SELECT 
                        ClassId,
                        COUNT(CASE WHEN Gender = 1 THEN 1 END) AS MaleCount,
                        COUNT(CASE WHEN Gender = 0 THEN 1 END) AS FemaleCount
                    FROM Student
                    GROUP BY ClassId
                )
                SELECT 
                    c.ClassId AS ClassId,
                    c.ClassName AS ClassName,
                    ISNULL(gc.MaleCount, 0) AS MaleCount,
                    ISNULL(gc.FemaleCount, 0) AS FemaleCount
                FROM Class c
                LEFT JOIN GenderCounts gc ON c.ClassId = gc.ClassId
                WHERE :ClassId = 0 OR c.ClassId = :ClassId
                ORDER BY c.ClassId
                ")
                .AddScalar("ClassId", NHibernateUtil.Int32)
                .AddScalar("ClassName", NHibernateUtil.String)
                .AddScalar("MaleCount", NHibernateUtil.Int32)
                .AddScalar("FemaleCount", NHibernateUtil.Int32)
                .SetParameter("ClassId", classId)
                .SetResultTransformer(Transformers.AliasToBean<GenderCountItem>());

            var result = await sqlQuery.ListAsync<GenderCountItem>();
            return (List<GenderCountItem>)result;
        }
    }
}
