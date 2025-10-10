// See https://aka.ms/new-console-template for more information
using System.Management;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceProcess;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WinSettingManager.Lib.LogonSession;
using WinSettingManager.Lib.Network;
using WinSettingManager.Lib.OSVersion;

/*
var networkInfos = NetworkAdapterInfo.Load();
var json = JsonSerializer.Serialize(networkInfos, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json);
*/

/*
var sessions = UserLogonSession.GetLoggedOnSession();
var json2 = JsonSerializer.Serialize(sessions, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json2);
*/
/*
var osversion = OSVersions.GetCurrent();
var json3 = JsonSerializer.Serialize(osversion, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json3);
*/

/*
var serviceSummaries = WinSettingManager.Lib.WindowsService.ServiceSummaryCollection.Load();
var json4 = JsonSerializer.Serialize(serviceSummaries, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
*/

/*
var services = ServiceController.GetServices();
var sc = services.FirstOrDefault(x => "TermService" == x.ServiceName);
var moobjects = new ManagementClass("Win32_Service").
        GetInstances().
        OfType<ManagementObject>();
*/
//var mo = moobjects.FirstOrDefault(x => sc.ServiceName == x["Name"] as string);
//Console.WriteLine(mo["Name"] as string);
/*
services.OrderBy(x => x.ServiceName).ToList().ForEach(x =>
{
    Console.WriteLine(x.ServiceName);
});
*/
/*
moobjects.OrderBy(x => x["Caption"] as string).ToList().ForEach(x =>
{
    Console.WriteLine(x["Caption"] as string);
});
*/
/*
Console.WriteLine(services.Length);
Console.WriteLine(moobjects.Count());
*/
/*
services.OrderBy(x => x.ServiceName).ToList().ForEach(x =>
{
    var mo = moobjects.FirstOrDefault(y => x.ServiceName == y["Name"] as string);
    Console.WriteLine(
        $"{x.ServiceName}\t{x.DisplayName}\t{(mo != null ? (mo["StartMode"] as string) : "N/A")}");
});
*/


/*
using (var rs = RunspaceFactory.CreateRunspace())
{
    rs.Open();
    using (var ps = PowerShell.Create())
    {

        ps.Runspace = rs;
        ps.AddCommand("Get-CimInstance").AddParameter("ClassName", "Win32_Service");
        foreach(var result in ps.Invoke())
        {
            Console.WriteLine(result.Properties["Name"].Value.ToString());
        }
    }
}
*/



/*
var serviceSimpleSummaries = WinSettingManager.Lib.WindowsService.ServiceSimpleSummaryCollection.Load();
var json5 = JsonSerializer.Serialize(serviceSimpleSummaries, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json5);
*/

var serviceSummaries = WinSettingManager.Lib.WindowsService.ServiceSummaryCollection.Load();
var json4 = JsonSerializer.Serialize(serviceSummaries, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json4);




Console.ReadLine();
