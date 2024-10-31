using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate.Linq;
using ISession = NHibernate.ISession;
namespace GrpcStudentManagementService.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ISession _session;

        public LevelRepository(ISession session)
        {
            _session = session;
        }

        public Task<List<Level>> GetAllLevelsAsync()
        {
            return _session.Query<Level>().ToListAsync();
        }
    }
}
