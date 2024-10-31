using FluentNHibernate.Mapping;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappings
{
    public class GradeMap : ClassMap<Grade>
    {
        public GradeMap()
        {
            Table("Grade");
            Id(x => x.GradeId, "GradeId");
            Map(x => x.GradeName, "GradeName");
            References(x => x.Level, "LevelName");
        }
    }
}
