using Application.Extensions;
using Infrastructure.EntityFramework.Extensions;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConections()
    .AddRepositories()
    .AddConfigureControllers()
    .AddUseCases()
    .AddCors(p => p.AddPolicy("CORS",
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORS");
app.MapControllers();
app.Run();
