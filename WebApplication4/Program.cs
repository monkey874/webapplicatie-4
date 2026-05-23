using WebApplication4.custumDebugger;
using WebApplication4.TrafficLight;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "stoplicht Systeem",   
        Version = "v1"
    });
});

var app = builder.Build();

app.UseCors("AllowAll");
CustumDebugger.Debugger(1, "Programma word opgestart" );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var worker = new Worker();
_ = worker.StartAsync();
app.MapControllers();
app.Run();

