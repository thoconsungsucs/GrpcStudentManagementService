using AutoMapper;
using Shared;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentShared>()
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Class != null ? src.Class.ClassId : 0))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.ClassName : string.Empty));

            CreateMap<StudentShared, Student>()
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => new Class { ClassId = src.ClassId, ClassName = string.Empty, Subject = string.Empty }));
        }
    }
}
