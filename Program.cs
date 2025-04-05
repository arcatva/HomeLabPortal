using Grpc.Net.Client;
using HomeLabPortal.Services;
using Scalar.AspNetCore;

namespace HomeLabPortal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment()) builder.Services.AddOpenApi();

        builder.Services.AddSingleton(
            () => GrpcChannel.ForAddress("https://localhost:5001")
        );

        builder.Services.AddGrpcClient<Spdk.SpdkClient>();

        builder.Services.AddScoped<SpdkService>();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapGet("/", () => "Hello world!");
            app.MapScalarApiReference();
            app.MapOpenApi();
        }

        app.MapControllers();

        app.Run();
    }
}