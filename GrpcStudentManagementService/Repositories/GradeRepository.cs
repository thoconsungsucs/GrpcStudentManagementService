using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate.Linq;
using ISession = NHibernate.ISession;
namespace GrpcStudentManagementService.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ISession _session;

        public GradeRepository(ISession session)
        {
            _session = session;
        }

        public Task<List<Grade>> GetGradesByLevelNameAsync(string levelName)
        {
            return _session.Query<Grade>().Where(g => g.Level.LevelName == levelName).ToListAsync();
        }

        public async Task<List<Grade>> ExecuteIQueryAbleAsync(IQueryable<Grade> queryable)
        {
            return await queryable.ToListAsync();
        }

        public IQueryable<Grade> GetAllAsIQueryAble()
        {
            return _session.Query<Grade>();
        }
    }
}
