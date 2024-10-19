using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate;
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

        public List<Class> GetAllClasses()
        {
            return _session.Query<Class>().ToList();
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
