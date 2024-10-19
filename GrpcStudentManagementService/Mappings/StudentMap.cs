using FluentNHibernate.Mapping;
using StudentManagement2.Model;

namespace GrpcStudentManagementService.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Student");
            Id(x => x.Id, "StudentId");
            Map(x => x.Name, "StudentName");
            Map(x => x.Dob, "Dob");
            Map(x => x.Address, "Address");
            References(x => x.Class, "ClassId");
        }
    }
}
