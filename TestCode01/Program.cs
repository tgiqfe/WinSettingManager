// See https://aka.ms/new-console-template for more information
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WinSettingManager.Items.LogonSession;
using WinSettingManager.Items.Network;
using WinSettingManager.Items.OSVersion;

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
var osversion = OSVersions.GetCurrent();
var json3 = JsonSerializer.Serialize(osversion, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json3);

Console.ReadLine();
