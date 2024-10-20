using GrpcStudentManagementService.Mappers;
using GrpcStudentManagementService.Repositories;
using GrpcStudentManagementService.Repositories.Interfaces;
using GrpcStudentManagementService.Services;
using NHibernate;
using ProtoBuf.Grpc.Server;
using Shared;

namespace GrpcStudentManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddGrpc();
            builder.Services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
            });

            builder.Services.AddSingleton(provider => NHibernateHelper.CreateSessionFactory());

            builder.Services.AddScoped(provider =>
            {
                var sessionFactory = provider.GetService<ISessionFactory>();
                return sessionFactory.OpenSession();
            });

            builder.Services.AddAutoMapper(typeof(StudentMapper));
            builder.Services.AddAutoMapper(typeof(ClassMapper));

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IClassRepository, ClassRepository>();

            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IClassService, ClassService>();

            var app = builder.Build();
            app.MapGrpcService<StudentService>();
            app.MapGrpcService<ClassService>();
            // Configure the HTTP request pipeline.
            // app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}