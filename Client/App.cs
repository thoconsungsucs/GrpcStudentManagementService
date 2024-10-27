using Shared;

namespace Client
{
    public class App
    {

        private readonly IClassService _classService;
        private readonly IStudentService _studentService;

        public App(IClassService classService, IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }

        public static void ShowMenu()
        {
            Console.WriteLine("1. Show all students");
            Console.WriteLine("2. Add student");
            Console.WriteLine("3. Change student's information");
            Console.WriteLine("4. Delete student");
            Console.WriteLine("5. Sort student list following name");
            Console.WriteLine("6. Search student by Id");
            Console.WriteLine("7. Exit");
        }

        public void Run()
        {
            while (true)
            {
                ShowMenu();
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        ShowAllStudents(true);
                        break;
                    case "6":
                        SearchStudentById();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        public void SearchStudentById()
        {
            Console.WriteLine("Enter student's id: ");
            var studentId = new RequestId { Value = int.Parse(Console.ReadLine()) };

            var result = _studentService.GetStudentById(studentId);
            if (result.IsSuccess)
            {
                var student = result.Value;
                Console.WriteLine($"Student Id: {student.StudentId}");
                Console.WriteLine($"Full StudentName: {student.StudentName}");
                Console.WriteLine($"Birthday: {student.Dob.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Address: {student.Address}");
                Console.WriteLine($"Class StudentName: {student.ClassName}");
                Console.WriteLine("--------------");
            }
            else
            {
                Console.WriteLine(result.Error);
            }
        }

        public void ShowAllStudents(bool sorted = false)
        {
            var result = _studentService.GetAllStudents();
            if (result.IsSuccess)
            {
                if (sorted)
                {
                    result.Value.OrderBy(s => s.StudentName).ToList();
                }
                foreach (var student in result.Value)
                {
                    Console.WriteLine($"Student Id: {student.StudentId}");
                    Console.WriteLine($"Full StudentName: {student.StudentName}");
                    Console.WriteLine($"Birthday: {student.Dob.ToString("dd/MM/yyyy")}");
                    Console.WriteLine($"Address: {student.Address}");
                    Console.WriteLine($"Class StudentName: {student.ClassName}");
                    Console.WriteLine("--------------");
                }
            }
            else
            {
                Console.WriteLine(result.Error);
            }
        }

        public void AddStudent()
        {
            Console.Write("Enter student's name: ");
            var name = Console.ReadLine();
            Console.Write("Enter student's dob: ");
            DateTime dob;
            DateTime.TryParse(Console.ReadLine(), out dob);
            Console.Write("Enter student's address: ");
            var address = Console.ReadLine();
            Console.Write("Enter student's ClassId: ");

            var classResult = _classService.GetAllClasses();
            if (classResult.IsSuccess)
            {
                foreach (var classs in classResult.Value)
                {
                    Console.WriteLine($"ClassId: {classs.ClassId}");
                    Console.WriteLine($"ClassName: {classs.ClassName}");
                    Console.WriteLine($"Subject: {classs.Subject}");
                    Console.WriteLine($"Teacher's name: {classs.TeacherName}");
                    Console.WriteLine("--------------");
                }
            }


            var student = new StudentShared
            {
                StudentName = name,
                Dob = dob,
                Address = address,
                ClassId = int.Parse(Console.ReadLine())
            };
            var result = _studentService.AddStudent(student);
            if (result.IsSuccess)
            {
                Console.WriteLine("Student added successfully");
            }
            else
            {
                Console.WriteLine(result.Error);
            }
        }

        public void ShowStudents()
        {
            var result = _studentService.GetAllStudents();
            if (result.IsSuccess)
            {
                result.Value.ForEach(s => Console.WriteLine(s.ToString()));
            }
            else
            {
                Console.WriteLine(result.Error);
            }
        }

        public void UpdateStudent()
        {
            Console.Write("Enter student's id: ");
            var studentId = int.Parse(Console.ReadLine());
            var studentResult = _studentService.GetStudentById(new RequestId { Value = studentId });
            if (!studentResult.IsSuccess)
            {
                Console.WriteLine(studentResult.Error);
                return;
            }

            var student = studentResult.Value;
            Console.Write("Enter student's name: ");
            student.StudentName = Console.ReadLine();

            Console.Write("Enter student's dob: ");
            DateTime dob;
            DateTime.TryParse(Console.ReadLine(), out dob);
            student.Dob = dob;

            Console.Write("Enter student's address: ");
            student.Address = Console.ReadLine();

            var classResult = _classService.GetAllClasses();
            if (classResult.IsSuccess)
            {
                foreach (var classs in classResult.Value)
                {
                    Console.WriteLine($"ClassId: {classs.ClassId}");
                    Console.WriteLine($"ClassName: {classs.ClassName}");
                    Console.WriteLine($"Subject: {classs.Subject}");
                    Console.WriteLine($"Teacher's name: {classs.TeacherName}");
                    Console.WriteLine("--------------");
                }
            }
            else
            {
                Console.WriteLine(classResult.Error);
                return;
            }

            Console.Write("Enter student's ClassId: ");
            student.ClassId = int.Parse(Console.ReadLine());

            var result = _studentService.UpdateStudent(student);
            if (result.IsSuccess)
            {
                Console.WriteLine("Student updated successfully");
            }
            else
            {
                Console.WriteLine(result.Error);
            }
        }

        public void DeleteStudent()
        {
            Console.Write("Enter student's id: ");
            var studentId = int.Parse(Console.ReadLine());

            var result = _studentService.DeleteStudent(new RequestId { Value = studentId });
            if (result.IsSuccess)
            {
                Console.WriteLine("Student deleted successfully");
            }
            else
            {
                Console.WriteLine(result.Error);
            }
        }

        public void SortStudentList()
        {
            var studentsResult = _studentService.GetAllStudents();
            if (studentsResult.IsSuccess)
            {
                studentsResult.Value.OrderBy(s => s.StudentName).ToList().ForEach(s => Console.WriteLine(s.ToString()));
            }
            else
            {
                Console.WriteLine(studentsResult.Error);
            }
        }

        /*private int GetClassId()
        {
            ClassShared? c = null;
            int ClassId = -1;
            while (c == null)
            {
                Console.Write("Enter student's class id (0 to skip): ");
                ClassId = int.Parse(Console.ReadLine());

                if (ClassId == 0)
                {
                    break;
                }

                c = _classService.GetClassById(ClassId);

                if (c == null)
                {
                    Console.WriteLine("Class not found. Please try again.");
                }
            }
            return c;
        }*/
    }
}
