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
            return query.Fetch(s => s.Class);
        }

        public async Task<List<Student>> GetAllPagination(StudentFilter studentFilter)
        {
            var query = Filter(studentFilter);
            return await query
                .OrderByDescending(s => s.StudentId)
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

        public async Task<List<PieChartItem>> CategoizeStudentAsync(StudentCategorizeOption option)
        {
            var query = _session.CreateSQLQuery(@$"
                    SELECT
                        COUNT(s.StudentId) AS Count,
                        g.LevelName
                        {(option.ByGrade || option.GradeId != 0 ? ", g.GradeName" : "")}
                        {(option.ByClass || option.GradeId != 0 ? ", c.ClassName" : "")}
                    FROM Student s
                    JOIN Class c ON s.ClassId = c.ClassId
                    JOIN (
                        SELECT GradeId, GradeName, LevelName
                        FROM Grade
                        WHERE (GradeId = :GradeId OR :GradeId = 0)
                          AND (LevelName = :LevelName OR :LevelName = '')
                    ) g ON c.GradeId = g.GradeId
                    GROUP BY g.LevelName
                        {(option.ByGrade || option.GradeId != 0 ? ", g.GradeName" : "")}
                        {(option.ByClass || option.GradeId != 0 ? ", c.ClassName" : "")}
                ");

            query.SetParameter("GradeId", option.GradeId);
            query.SetParameter("LevelName", option.LevelName);

            var list = await query.ListAsync<object[]>();
            var res = new List<PieChartItem>();


            foreach (var item in list)
            {
                res.Add(new PieChartItem
                {
                    type = item[item.Length - 1].ToString(),
                    value = int.Parse(item[0].ToString()),
                });
            }
            return res;
        }


        public async Task<List<NameAndCount>> GetStudentCountGroupByGradeAsync()
        {
            var studentByGradeList = await _session.Query<Student>()
                .GroupBy(s => s.Class.Grade.GradeName)
                .Select(g => new NameAndCount
                {
                    Name = g.Key,
                    Count = g.Count()
                }).ToListAsync();
            return studentByGradeList;
        }

        public async Task<List<NameAndCount>> GetStudentCountGroupByLevelAsync()
        {
            var studentByLevelList = await _session.Query<Student>()
                .GroupBy(s => s.Class.Grade.Level.LevelName)
                .Select(g => new NameAndCount
                {
                    Name = g.Key,
                    Count = g.Count()
                }).ToListAsync();
            return studentByLevelList;
        }

        public async Task<List<NameAndCount>> GetStudentCountGroupByLastGradeAsync()
        {
            var lastGrade = await _session.Query<Student>()
                .Where(s => CommonType.LastGrades.Contains(s.Class.Grade.GradeName))
                .GroupBy(s => s.Class.Grade.GradeName)
                .OrderBy(g => g.Key)
                .Select(g => new NameAndCount
                {
                    Name = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
            return lastGrade;
        }

        
    }
}
