using AutoMapper;
using GrpcStudentManagementService.Repositories.Interfaces;
using Shared;
using Shared.Exceptions;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Services
{
    public class StudentService : IStudentService
    {
        public IStudentRepository _studentRepository;
        public IClassRepository _classRepository;
        public IMapper _studentMapper;
        public IMapper _classMapper;
        public ILogger<StudentService> _logger;

        public StudentService(
            IStudentRepository studentRepository,
            IMapper studentMapper,
            IMapper classMapper,
            IClassRepository classRepository,
            ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _studentMapper = studentMapper;
            _classMapper = classMapper;
            _classRepository = classRepository;
            _logger = logger;
        }

        public Result AddStudent(StudentShared studentShared)
        {
            try
            {
                var isAnyClass = _classRepository.IsAny(studentShared.ClassId);
                if (!isAnyClass)
                {
                    return StudentError.StudentClassNotFound(studentShared.ClassId);
                }

                var student = _studentMapper.Map<Student>(studentShared);
                _studentRepository.AddStudent(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");
                return ex.Message;
            }
            return Result.Success();
        }

        public Result DeleteStudent(RequestId requestId)
        {
            try
            {
                var student = _studentRepository.GetStudentById(requestId.Value);

                if (student == null)
                {
                    return StudentError.StudentClassNotFound(requestId.Value);
                }

                _studentRepository.DeleteStudent(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");
                return ex.Message;
            }
            return Result.Success();
        }

        public async Task<Result<ListInfo<StudentShared>>> GetAllPaginationAsync(PaginationRequest request)
        {
            try
            {
                var students = await _studentRepository.GetAllPagination(request.PageIndex, request.PageSize);
                var Total = _studentRepository.CountAsync();
                var res = new ListInfo<StudentShared>
                {
                    List = _studentMapper.Map<List<StudentShared>>(students),
                    Total = Total.Result
                };
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all students info");
                return ex.Message;
            }
        }

        public Result<List<StudentShared>> GetAllStudents()
        {
            try
            {
                var students = _studentRepository.GetAllStudents();
                var mapped = _studentMapper.Map<List<StudentShared>>(students);
                var res = new Result<List<StudentShared>>(_studentMapper.Map<List<StudentShared>>(students), true, string.Empty);
                return _studentMapper.Map<List<StudentShared>>(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all students");
                return ex.Message;
            }
        }

        public Result<StudentShared> GetStudentById(RequestId requestId)
        {
            try
            {
                var student = _studentRepository.GetStudentById(requestId.Value);

                if (student == null)
                {
                    return StudentError.StudentNotFound(requestId.Value);
                }

                return _studentMapper.Map<StudentShared>(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student by id");
                return ex.Message;
            }
        }

        public Result UpdateStudent(StudentShared studentShared)
        {
            try
            {
                var student = _studentRepository.GetStudentById(studentShared.StudentId);

                if (student == null)
                {
                    return StudentError.StudentNotFound(studentShared.StudentId);
                }

                student.StudentName = studentShared.StudentName;
                student.Address = studentShared.Address;
                student.Dob = studentShared.Dob;

                var classs = _classRepository.GetClassById(studentShared.ClassId);
                if (classs == null)
                {
                    return StudentError.StudentClassNotFound(studentShared.ClassId);
                }

                student.Class = classs;

                _studentRepository.UpdateStudent(student);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating student");
                return ex.Message;
            }
        }

        public async Task<Result> AddAsync(StudentShared studentShared)
        {
            try
            {
                var isAnyClass = _classRepository.IsAny(studentShared.ClassId);
                if (!isAnyClass)
                {
                    return StudentError.StudentClassNotFound(studentShared.ClassId);
                }

                var student = _studentMapper.Map<Student>(studentShared);
                await _studentRepository.AddStudentAsync(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");
                return ex.Message;
            }
            return Result.Success();

        }

        public async Task<Result> UpdateAsync(StudentShared studentShared)
        {
            try
            {
                var student = await _studentRepository.GetStudentByIdAsync(studentShared.StudentId);

                if (student == null)
                {
                    return StudentError.StudentNotFound(studentShared.StudentId);
                }

                student.StudentName = studentShared.StudentName;
                student.Address = studentShared.Address;
                student.Dob = studentShared.Dob;
                student.Gender = studentShared.Gender;
                var classs = await _classRepository.GetClassByIdAsync(studentShared.ClassId);
                if (classs == null)
                {
                    return StudentError.StudentClassNotFound(studentShared.ClassId);
                }

                student.Class = classs;

                await _studentRepository.UpdateStudentAsync(student);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating student");
                return ex.Message;
            }
        }

        public async Task<Result> DeleteAsync(RequestId requestId)
        {
            try
            {
                var student = await _studentRepository.GetStudentByIdAsync(requestId.Value);

                if (student == null)
                {
                    return StudentError.StudentClassNotFound(requestId.Value);
                }

                await _studentRepository.DeleteStudentAsync(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");
                return ex.Message;
            }
            return Result.Success();
        }

        public async Task<Result<List<BarChartItem>>> GetGenderCountAsync(RequestId? classRequestId = null)
        {
            try
            {
                int classId = classRequestId != null ? classRequestId.Value : 0;
                var genderCounts = await _studentRepository.GetGenderCountAsync(classId);
                var res = new List<BarChartItem>();
                foreach (var item in genderCounts)
                {
                    res.Add(new BarChartItem
                    {
                        Label = item.ClassName,
                        Type = Gender.Male.ToString(),
                        Value = item.MaleCount
                    });
                    res.Add(new BarChartItem
                    {
                        Label = item.ClassName,
                        Type = Gender.Female.ToString(),
                        Value = item.FemaleCount
                    });
                }
                return res;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting gender count");
                return ex.Message;
            }
        }
    }
}
