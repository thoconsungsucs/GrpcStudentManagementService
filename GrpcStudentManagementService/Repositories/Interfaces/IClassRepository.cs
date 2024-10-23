using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Repositories.Interfaces
{
    public interface IClassRepository
    {
        public List<Class> GetAllClasses();
        public IQueryable<Class> GetAllAsIQueryAble();
        public Task<List<Class>> ExecuteIQueryAbleAsync(IQueryable<Class> queryable);
        public Class? GetClassById(int classId);
        public bool IsAny(int classId);

        public Task<Class> GetClassByIdAsync(int classId);
    }
}
