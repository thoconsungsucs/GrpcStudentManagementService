using AutoMapper;
using GrpcStudentManagementService.Models;
using Shared;

namespace GrpcStudentManagementService.Mappers
{
    public class LevelMapper : Profile
    {
        public LevelMapper()
        {
            CreateMap<Level, LevelShared>()
                .ReverseMap();
        }
    }
}
