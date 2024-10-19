using AutoMapper;
using FluentNHibernate.Mapping;
using GrpcStudentManagementService.Models;
using Shared;

namespace GrpcStudentManagementService.Mappers
{
    public class ClassMapper : Profile
    {
        public ClassMapper() 
        {
            CreateMap<ClassShared, Class>()
                .ReverseMap();
        }
    }
}
