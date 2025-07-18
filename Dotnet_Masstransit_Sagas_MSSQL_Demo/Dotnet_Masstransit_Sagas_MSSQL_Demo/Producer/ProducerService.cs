using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;
using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Producer;

public class ProducerService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProducerService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var evt = new StageOneEvent
        {
            Name = "Hello World"
        };

        Console.WriteLine($"Publishing StageOneEvent with CorrelationId: {evt.CorrelationId}");
        
        await _publishEndpoint.Publish(evt, cancellationToken);
    }
}
