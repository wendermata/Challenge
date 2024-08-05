using Application.Extensions;
using Infrastructure.EntityFramework.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUseCases()
    .AddRepositories()
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
