using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate.Linq;
using ISession = NHibernate.ISession;
namespace GrpcStudentManagementService.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ISession _session;

        public ClassRepository(ISession session)
        {
            _session = session;
        }

        public async Task<List<Class>> ExecuteIQueryAbleAsync(IQueryable<Class> queryable)
        {
            return await queryable.ToListAsync();
        }

        public List<Class> GetAllClasses()
        {
            return _session.Query<Class>().ToList();
        }

        public IQueryable<Class> GetAllAsIQueryAble()
        {
            return _session.Query<Class>();
        }

        public Class? GetClassById(int classId)
        {
            return _session.Get<Class>(classId);
        }

        public bool IsAny(int classId)
        {
            return _session.Query<Class>()
                .Any(c => c.ClassId == classId);
        }

    }
}
