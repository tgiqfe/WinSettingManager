using Receiver.Lib;
using WinSettingManager.Items.Network;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// empty route handlers
app.MapGet("/", () => "");
app.MapPost("/", () => "");
app.MapPut("/", () => "");
app.MapDelete("/", () => "");

//  api setting
//  v1
var api_v1 = "/api/v1";

app.MapGet($"{api_v1}/system/info", () => "GET v1");
app.MapGet($"{api_v1}/network/info", () => async (NetworkAdapter networkAdapterInfo) =>
{
    return await NetworkMethods.GetNetworkAdapterInfo();
});



app.Run();
