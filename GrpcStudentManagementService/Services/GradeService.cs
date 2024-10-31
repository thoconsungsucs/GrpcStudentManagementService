using AutoMapper;
using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories;
using GrpcStudentManagementService.Repositories.Interfaces;
using Shared;
using Shared.Exceptions;

namespace GrpcStudentManagementService.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public IMapper _gradeMapper;
        public GradeService(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _gradeMapper = mapper;
        }

        public async Task<Result<List<GradeShared>>> GetGradesByLevelNameAsync(string LevelName)
        {
            try
            {
                var grades = await _gradeRepository.GetGradesByLevelNameAsync(LevelName);
                if (grades == null)
                {
                    return GradeError.GradeNotFound(LevelName);
                }
                return _gradeMapper.Map<List<GradeShared>>(grades);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        

        public async Task<Result<List<SelectionItem>>> GetGradeSelectionByLevelNameAsync(string levelName)
        {
            var query = _gradeRepository.GetAllAsIQueryAble().Where(g => g.Level.LevelName == levelName).Select(g => new Grade
            {
                GradeId = g.GradeId,
                GradeName = g.GradeName,
            });
            var classSelections = (await _gradeRepository.ExecuteIQueryAbleAsync(query)).Select(g => new SelectionItem
            {
                Id = g.GradeId,
                Name = g.GradeName,
            }).ToList();
            return classSelections;

        }
    }
}
