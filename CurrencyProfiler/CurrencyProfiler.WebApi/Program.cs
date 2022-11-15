using CurrencyProfiler.WebApi;
using CurrencyProfiler.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o => o.AddPolicy("AllPolicy", policy =>
{
    policy.AllowAnyOrigin(); 
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));
builder.Services.AddControllers();
builder.Services.AddPersistence();
builder.Services.AddApplication();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseCors("AllPolicy");
app.MapControllers();

app.Run();