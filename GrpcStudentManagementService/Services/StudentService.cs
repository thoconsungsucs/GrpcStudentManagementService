using AutoMapper;
using GrpcStudentManagementService.Models;
using GrpcStudentManagementService.Repositories.Interfaces;
using Shared;

namespace GrpcStudentManagementService.Services
{
    public class StudentService : IStudentService
    {
        public IStudentRepository _studentRepository;
        public IClassRepository _classRepository;
        public IMapper _studentMapper;
        public IMapper _classMapper;

        public StudentService(
            IStudentRepository studentRepository, 
            IMapper studentMapper, 
            IMapper classMapper,
            IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _studentMapper = studentMapper;
            _classMapper = classMapper;
            _classRepository = classRepository;
        }

        public void AddStudent(StudentShared studentShared)
        {
            var isAnyClass = _classRepository.IsAny(studentShared.ClassId);
            if (!isAnyClass)
            {
                throw new Exception("Class does not exist");
            }
            var student = _studentMapper.Map<Student>(studentShared);
            _studentRepository.AddStudent(student);
        }

        public void DeleteStudent(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            if (student == null)
            {
                throw new Exception($"Student {studentId} does not exist");
            }
            _studentRepository.DeleteStudent(student);
        }

        public List<StudentShared> GetAllStudents()
        {
            var students = _studentRepository.GetAllStudents();
            return _studentMapper.Map<List<StudentShared>>(students);
        }

        public StudentShared? GetStudentById(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            return _studentMapper.Map<StudentShared>(student);
        }

        public void UpdateStudent(StudentShared studentShared)
        {
            var student = _studentRepository.GetStudentById(studentShared.StudentId);
            if (student == null)
            {
                throw new Exception($"Student {studentShared.StudentId} does not exist");
            }
            student.StudentName = studentShared.StudentName;
            student.Address = studentShared.Address;

            var classs = _classRepository.GetClassById(studentShared.ClassId);
            if (classs == null)
            {
                throw new Exception("");
            }
            _studentRepository.UpdateStudent(student);
        }
    }
}
