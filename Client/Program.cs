using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton(services =>
            {
                // Get the service address from appsettings.json
                //var config = services.GetRequiredService<IConfiguration>();
                //var backendUrl = config["BackendUrl"];

                var backendUrl = "https://localhost:7005";
                // If no address is set then fallback to the current webpage URL
                if (string.IsNullOrEmpty(backendUrl))
                {
                    var navigationManager = services.GetRequiredService<NavigationManager>();
                    backendUrl = navigationManager.BaseUri;
                }

                // Create a channel with a GrpcWebHandler that is addressed to the backend server.
                //
                // GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
                // then GrpcWeb is recommended because it produces smaller messages.

                return GrpcChannel.ForAddress(
                    backendUrl,
                    new GrpcChannelOptions
                    {
                        //CompressionProviders = ...,
                        //Credentials = ...,
                        //DisposeHttpClient = ...,
                        //HttpClient = ...,
                        //LoggerFactory = ...,
                        //MaxReceiveMessageSize = ...,
                        //MaxSendMessageSize = ...,
                        //ThrowOperationCanceledOnCancellation = ...,
                    });
            });

            services.AddTransient<IStudentService>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<IStudentService>();
            });

            services.AddTransient<IClassService>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<IClassService>();
            });


            services.AddSingleton<App>();

            var serviceProvider = services.BuildServiceProvider();
            var app = serviceProvider.GetRequiredService<App>();
            app.Run();
        }
    }
}
