using AutoMapper;
using Shared;
using GrpcStudentManagementService.Models;

namespace GrpcStudentManagementService.Mappers
{
    public class ClassMapper : Profile
    {
        public ClassMapper()
        {
            CreateMap<Class, ClassShared>()
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.TeacherId : 0))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.TeacherName : string.Empty))
                .ReverseMap();


        }
    }
}
