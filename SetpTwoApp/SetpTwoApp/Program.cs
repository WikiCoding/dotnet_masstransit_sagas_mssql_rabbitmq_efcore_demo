using MassTransit;
using SetpTwoApp.Consumers;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<StepTwoConsumer>();

    cfg.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration["Broker:Host"] ?? "amqp://localhost:5672"), h =>
        {
            h.Username(builder.Configuration["Broker:Username"] ?? "admin");
            h.Password(builder.Configuration["Broker:Password"] ?? "admin");
        });

        config.ReceiveEndpoint("stages-saga-model", e =>
        {
            e.Bind("Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents:StageTwoEvent", x =>
            {
                x.ExchangeType = "fanout";
            });

            e.ConfigureConsumer<StepTwoConsumer>(context);
        });

        config.Message<StageTwoEvent>(x =>
        {
            x.SetEntityName("Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents:StageTwoEvent");
        });

        config.Message<StageThreeEvent>(x =>
        {
            x.SetEntityName("Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents:StageThreeEvent");
        });

        config.Publish<StageThreeEvent>(x =>
        {
            x.ExchangeType = "fanout";
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
