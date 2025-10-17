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
    SystemMethods.GetSystemInfoAsync());




app.MapGet($"{api_v1}/application/version", async () =>
    await ApplicationMethods.GetVersionAsync());
app.MapGet($"{api_v1}/application/exit", () =>
    ApplicationMethods.ExitApplicationAsync());
app.MapPost($"{api_v1}/application/exit", () =>
    ApplicationMethods.ExitApplicationAsync());

//  Local User Account
app.MapGet($"{api_v1}/localaccount/user/list", () =>
    LocalAccountMethods.GetLocalUsersAsync());
app.MapGet($"{api_v1}/localaccount/group/list", () =>
    LocalAccountMethods.GetLocalGroupsAsync());

//  localaccount/user/add/{{name}}
//  localaccount/user/delete/{{name}}
//  localaccount/user/changepassword/{{name}}


//  Logon Sessions
app.MapGet($"{api_v1}/logonsession/list", () =>
    LogonSessionMethods.GetLogonSessionsAsync());
app.MapPost($"{api_v1}/logonsession/user", (LogonSessionDataContact contact) =>
    LogonSessionMethods.SetLogonSessionsAsync(contact));

//  Sound Volume
app.MapGet($"{api_v1}/sound/volume", async () =>
    await SoundMethods.GetSoundVolumeAsync());
app.MapPost($"{api_v1}/sound/volume", async (SoundVolumeDataContact contact) =>
    await SoundMethods.SetSoundVolumeAsync(contact));

//  Windows Service
app.MapGet($"{api_v1}/service/list", async () =>
    await WindowsServiceMethods.GetServiceSummariesAsync());
app.MapGet($"{api_v1}/service/list/{{name}}", async (string name) =>
    await WindowsServiceMethods.GetServiceSummariesAsync(name));
app.MapGet($"{api_v1}/service/simple/list", async () =>
    await WindowsServiceMethods.GetServiceSimpleSummariesAsync());
app.MapGet($"{api_v1}/service/simple/list/{{name}}", async (string name) =>
    await WindowsServiceMethods.GetServiceSimpleSummariesAsync(name));


app.MapPost($"{api_v1}/registry/key", (HttpRequest req) => "");
app.MapPost($"{api_v1}/registry/parameter", (HttpRequest req) => "");



//  Network Information
app.MapGet($"{api_v1}/network/info", async () =>
    await NetworkMethods.GetNetworkAdapterCollectionAsync());



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


