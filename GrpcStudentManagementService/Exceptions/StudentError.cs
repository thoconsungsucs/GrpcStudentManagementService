using FluentNHibernate.Conventions.Inspections;

namespace GrpcStudentManagementService.Exceptions
{
    public static class StudentError
    {
        public static string StudentNotFound(string studentId) => $"Student {studentId} does not exist";
        public static string StudentClassNotFound(string classId) => $"Student {classId} does not exist";

    }
}
