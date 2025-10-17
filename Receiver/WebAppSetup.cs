using Microsoft.Extensions.Hosting.WindowsServices;

namespace Receiver
{
    public class WebAppSetup
    {
        public static WebApplication Initialize(string[] args)
        {
            //  Prepare Web Application.
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //  Configure JSON options
            //  - to handle enums as strings
            //  - ignore null values.
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            //  Configure as Windows Service.
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "WinSetMng";
            });
            if (WindowsServiceHelpers.IsWindowsService())
            {
                builder.Services.AddSingleton<IHostLifetime, Receiver.ServiceLifeTime>();
            }

            var app = builder.Build();

            //  Configure the HTTP request pipeline. (Swagger)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
