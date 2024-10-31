using FluentNHibernate.Mapping;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappings
{
    public class LevelMap : ClassMap<Level>
    {
        public LevelMap()
        {
            Table("Level");
            Id(x => x.LevelName, "LevelName");
        }
    }
}
