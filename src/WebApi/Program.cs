using Application.Extensions;
using Infra.Kafka.Consumer.Extensions;
using Infra.Kafka.Producer.Extensions;
using Infra.Mongo.Extensions;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfigureControllers()
    .AddUseCases()
    .AddKakfaConsumers()
    .AddKakfaProducers(builder.Configuration)
    .AddBoundaries(builder.Configuration)
    .AddMongo(builder.Configuration)
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
