using FluentNHibernate.Mapping;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappings
{
    public class ClassMap : ClassMap<Class>
    {
        public ClassMap()
        {
            Table("Class");
            Id(x => x.ClassId, "ClassId");
            Map(x => x.ClassName, "ClassName");
            Map(x => x.Subject, "Subject");
            References(x => x.Teacher, "TeacherId");
        }
    }
}
