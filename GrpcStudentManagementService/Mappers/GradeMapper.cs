using AutoMapper;
using GrpcStudentManagementService.Models;
using Shared;

namespace GrpcStudentManagementService.Mappers
{
    public class GradeMapper : Profile
    {
        public GradeMapper()
        {
            CreateMap<Grade, GradeShared>()
                .ReverseMap();
        }
    }
}
