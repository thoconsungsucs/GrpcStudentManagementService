using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Repositories.Interfaces
{
    public interface IClassRepository
    {
        public List<Class> GetAllClasses();
        public Class? GetClassById(int classId);
        public bool IsAny(int classId);
    }
}
