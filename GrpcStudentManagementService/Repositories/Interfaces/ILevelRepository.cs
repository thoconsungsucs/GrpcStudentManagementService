using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Repositories.Interfaces
{
    public interface ILevelRepository
    {
        public Task<List<Level>> GetAllLevelsAsync();
    }
}
