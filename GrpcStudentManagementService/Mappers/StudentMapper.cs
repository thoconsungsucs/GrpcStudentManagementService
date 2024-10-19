using AutoMapper;
using GrpcStudentManagementService.Models;
using Shared;

namespace GrpcStudentManagementService.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper() 
        {
            CreateMap<Student, StudentShared>()
                .ReverseMap();

        }
    }
}
