using Dotnet_Masstransit_Sagas_MSSQL_Demo.Producer;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.DataModels;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddSagaStateMachine<StagesStateMachine, StagesSagaModel>()
    .EntityFrameworkRepository(r =>
    {
        r.ConcurrencyMode = ConcurrencyMode.Optimistic;
        r.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
        r.AddDbContext<StagesDbContext, StagesDbContext>((provider, ctxBuilder) =>
        {
            ctxBuilder.UseSqlServer(builder.Configuration.GetConnectionString("mssql"));
        });
    });

    config.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration["Broker:Host"] ?? "amqp://localhost:5672"), h =>
        {
            h.Username(builder.Configuration["Broker:Username"] ?? "admin");
            h.Password(builder.Configuration["Broker:Password"] ?? "admin");
        });

        config.UseInMemoryOutbox(context);

        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ProducerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
