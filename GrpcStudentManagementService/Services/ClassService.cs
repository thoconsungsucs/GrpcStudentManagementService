using AutoMapper;
using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using Shared;
using Shared.Exceptions;

namespace GrpcStudentManagementService.Services
{
    public class ClassService : IClassService
    {
        public IClassRepository _classRepository;
        public IMapper _classMapper;
        public ClassService(IClassRepository classRepository, IMapper classMapper)
        {
            _classRepository = classRepository;
            _classMapper = classMapper;
        }

        public Result<List<ClassShared>> GetAllClasses()
        {
            var classes = _classRepository.GetAllClasses();
            return _classMapper.Map<List<ClassShared>>(classes);
        }

        public Result<ClassShared> GetClassById(RequestId requestId)
        {
            var classs = _classRepository.GetClassById(requestId.Value);
            if (classs == null)
            {
                return ClassError.ClassNotFound(requestId.Value);
            }
            return _classMapper.Map<ClassShared>(_classRepository.GetClassById(requestId.Value));
        }

        public async Task<Result<List<SelectionItem>>> GetClassSelectionAsync()
        {
            var query = _classRepository.GetAllAsIQueryAble().Select(c => new Class
            {
                ClassId = c.ClassId,
                ClassName = c.ClassName,
            });
            var classSelections = (await _classRepository.ExecuteIQueryAbleAsync(query)).Select(c => new SelectionItem
            {
                Id = c.ClassId,
                Name = c.ClassName,
            }).ToList();
            return classSelections;

        }
    }
}
