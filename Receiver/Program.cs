using Microsoft.Extensions.Hosting.WindowsServices;
using Receiver;
using Receiver.DataContact;
using Receiver.Functions;
using System.Diagnostics;

//  Initialize Web Application.
var app = WebAppSetup.Initialize(args);

//  api v1 text
var api_v1 = "/api/v1";

//  ################################################################
//  ##                      API Endpoints                         ##
//  ################################################################


// empty route handlers
#if DEBUG
app.MapGet("/", () => "");
app.MapPost("/", () => "");
app.MapPut("/", () => "");
app.MapDelete("/", () => "");
#endif

//  Application Information
app.MapGet($"{api_v1}/application/version", async () =>
    await ApplicationMethods.GetVersionAsync());
app.MapGet($"{api_v1}/application/exit", () =>
    ApplicationMethods.ExitApplicationAsync());
app.MapPost($"{api_v1}/application/exit", () =>
    ApplicationMethods.ExitApplicationAsync());

//  System Information
app.MapGet($"{api_v1}/system/info", () =>
    SystemMethods.GetSystemInfoAsync());

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

//  service/start
//  service/stop
//  service/restart
//  service/starttype


//  Windows Registry
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
