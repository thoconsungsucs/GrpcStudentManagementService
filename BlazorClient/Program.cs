using BlazorClient.Components;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using ProtoBuf.Grpc.Client;
using Shared;

namespace BlazorClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddAntDesign();
            builder.Services.AddSingleton(services =>
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

            builder.Services.AddTransient<IStudentService>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<IStudentService>();
            });

            builder.Services.AddTransient<IClassService>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<IClassService>();
            });

            builder.Services.AddTransient<IGradeService>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<IGradeService>();
            });

            builder.Services.AddTransient<ILevelService>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<ILevelService>();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}