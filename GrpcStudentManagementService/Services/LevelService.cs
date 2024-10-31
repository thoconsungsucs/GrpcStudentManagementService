using AutoMapper;
using GrpcStudentManagementService.Repositories.Interfaces;
using Shared;
using Shared.Exceptions;

namespace GrpcStudentManagementService.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        public IMapper _levelMapper;
        public LevelService(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _levelMapper = mapper;
        }

        public async Task<Result<List<LevelShared>>> GetAllLevelsAsync()
        {
            try
            {
                var levels = await _levelRepository.GetAllLevelsAsync();    
                return _levelMapper.Map<List<LevelShared>>(levels);
            }
            catch (Exception ex)
            {
                return "Error when getting levels.";
            }
        }
    }
}
