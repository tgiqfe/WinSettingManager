// See https://aka.ms/new-console-template for more information
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WinSettingManager.Items.Network;

var networkInfos = NetworkAdapterInfo.Load();

var json = JsonSerializer.Serialize(networkInfos, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
});
Console.WriteLine(json);

Console.ReadLine();
