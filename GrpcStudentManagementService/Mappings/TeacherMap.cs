using FluentNHibernate.Mapping;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappings
{
    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Table("Teacher");
            Id(x => x.TeacherId, "TeacherId");
            Map(x => x.TeacherName, "TeacherName");
            Map(x => x.Dob, "Dob");
        }
    }
}
