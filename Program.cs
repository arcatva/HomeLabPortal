using Grpc.Net.Client;
using HomeLabDashboard.Services;
using Scalar.AspNetCore;

namespace HomeLabDashboard;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173") // Allow frontend origin
                            .AllowAnyHeader() // Or specify allowed headers
                            .AllowAnyMethod(); // Or specify allowed methods
                        // policy.AllowCredentials(); // If needed
                    });
            });
            builder.Services.AddOpenApi();
        }

        builder.Services.AddGrpcClient<Spdk.SpdkClient>(options =>
        {
            options.Address = new Uri(builder.Configuration["GRPC_URL"]!);
        });

        builder.Services.AddScoped<SpdkService>();
        builder.Services.AddControllers();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapGet("/", () => "Hello world!");
            app.MapScalarApiReference();
            app.MapOpenApi();
            app.UseCors("MyAllowSpecificOrigins");
        }

        app.UsePathBase("/dashboard");
        app.UseRouting();
        app.UseStaticFiles();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.Run();
    }
}