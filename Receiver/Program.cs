using Microsoft.Extensions.Hosting.WindowsServices;
using Receiver.DataContact;
using Receiver.Functions;
using System.Diagnostics;


//  Prepare Web Application.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "WinSetMng";
});
if (WindowsServiceHelpers.IsWindowsService())
{
    builder.Services.AddSingleton<IHostLifetime, Receiver.ServiceLifeTime>();
}


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// empty route handlers
#if DEBUG
app.MapGet("/", () => "");
app.MapPost("/", () => "");
app.MapPut("/", () => "");
app.MapDelete("/", () => "");
#endif

//  api setting
//  v1
var api_v1 = "/api/v1";

//  System Information
app.MapGet($"{api_v1}/system/info", () =>
    SystemMethods.GetSystemInfo());
app.MapGet($"{api_v1}/system/hostname", () =>
    SystemMethods.GetHostName());
app.MapGet($"{api_v1}/system/domainname", () =>
    SystemMethods.GetDomainName());
app.MapGet($"{api_v1}/system/osversion", () =>
    SystemMethods.GetOSVersion());

#if DEBUG
app.MapPost($"{api_v1}/system/exit", () =>
{
    Task.Run(() =>
    {
        Task.Delay(1000);
        Environment.Exit(0);
    }).ConfigureAwait(false);
    return "";
});
#endif

//  Local User Account
app.MapGet($"{api_v1}/localaccount/user/list", () =>
    LocalAccountMethods.GetLocalUsers());
app.MapGet($"{api_v1}/localaccount/group/list", () =>
    LocalAccountMethods.GetLocalGroups());

//  Logon Sessions
app.MapGet($"{api_v1}/logonsession/list", () =>
    LogonSessionMethods.GetLogonSessions());
app.MapPost($"{api_v1}/logonsession/user", (DataContactLogonSession contact) =>
    LogonSessionMethods.SetLogonSessions(contact));

//  Sound Volume
app.MapGet($"{api_v1}/sound/volume", async () =>
    await SoundMethods.GetSoundVolume());
app.MapPost($"{api_v1}/sound/volume", async (DataContactSoundVolume contact) =>
    await SoundMethods.SetSoundVolume(contact));

//  Windows Service
app.MapGet($"{api_v1}/service/list", async () =>
    await WindowsServiceMethods.GetServiceSummaries());
app.MapGet($"{api_v1}/service/list/{{name}}", async (string name) =>
    await WindowsServiceMethods.GetServiceSummaries(name));
app.MapGet($"{api_v1}/service/simple/list", async () =>
    await WindowsServiceMethods.GetServiceSimpleSummaries());
app.MapGet($"{api_v1}/service/simple/list/{{name}}", async (string name) =>
    await WindowsServiceMethods.GetServiceSimpleSummaries(name));


app.MapPost($"{api_v1}/registry/key", (HttpRequest req) => "");
app.MapPost($"{api_v1}/registry/parameter", (HttpRequest req) => "");



//  Network Information
app.MapGet($"{api_v1}/network/info", async () =>
    await NetworkMethods.GetNetworkAdapterCollection());



//  Test Code
app.MapGet($"{api_v1}/test/command/{{text}}", async (string text) =>
{
    return await Task.Run(() =>
    {
        using (Process process = new Process())
        {
            process.StartInfo.FileName = text;
            process.StartInfo.Arguments = "";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    });
});




app.Run("http://*:5000");


