using User.Api.Core;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("RandomUser", client =>
{
    client.BaseAddress = new Uri("https://randomuser.me/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

Datebase.Instance = new MemoryDatabase();

builder.Services.AddCore();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
