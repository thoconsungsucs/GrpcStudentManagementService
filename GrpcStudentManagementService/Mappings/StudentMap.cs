using FluentNHibernate.Mapping;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Student");
            Id(x => x.StudentId, "StudentId");
            Map(x => x.StudentName, "StudentName");
            Map(x => x.Dob, "Dob");
            Map(x => x.Address, "Address");
            References(x => x.Class, "ClassId");
        }
    }
}
