using AutoMapper;
using GrpcStudentManagementService.Repositories.Interfaces;
using NHibernate;
using Shared;

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

        public List<ClassShared> GetAllClasses()
        {
            var classes = _classRepository.GetAllClasses();
            return _classMapper.Map<List<ClassShared>>(classes);
        }

        public ClassShared? GetClassById(int classId)
        {
            return _classMapper.Map<ClassShared>(_classRepository.GetClassById(classId));
        }
    }
}
