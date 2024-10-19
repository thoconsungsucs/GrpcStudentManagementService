using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IClassService
    {
        public ClassShared? GetClassById(int classId);
        public List<ClassShared> GetAllClasses();
    }

    public class ClassShared
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Subject { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
    }

}
