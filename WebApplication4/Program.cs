using Microsoft.AspNetCore.Builder;
using WebApplication4;


var builder = WebApplication.CreateBuilder(args);

// Controllers inschakelen
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

// Swagger (optioneel)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAll");

// Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirect uitzetten (anders krijg je errors)
//// app.UseHttpsRedirection();

// Controllers activeren
app.MapControllers();
app.Run();


