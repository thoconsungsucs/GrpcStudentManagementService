using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        public Task<List<Grade>> GetGradesByLevelNameAsync(string levelName);
        public IQueryable<Grade> GetAllAsIQueryAble();
        public Task<List<Grade>> ExecuteIQueryAbleAsync(IQueryable<Grade> queryable);
    }
}
