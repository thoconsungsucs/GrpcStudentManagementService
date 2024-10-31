
namespace Shared.Exceptions
{
    public static class StudentError
    {
        public static string StudentNotFound(int studentId) => $"Student {studentId} does not exist";
        public static string StudentClassNotFound(int classId) => $"Class {classId} does not exist";
    }

    public static class ClassError
    {
        public static string ClassNotFound(int classId) => $"Class {classId} does not exist";
    }

    public static class GradeError
    {
        public static string GradeNotFound(string levelName) => $"Grade {levelName} does not exist";
    }
}
