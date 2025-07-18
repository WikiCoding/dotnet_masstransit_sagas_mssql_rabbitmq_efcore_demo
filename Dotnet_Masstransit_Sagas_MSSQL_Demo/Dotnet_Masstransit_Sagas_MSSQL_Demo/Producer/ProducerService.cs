using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;
using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Producer;

public class ProducerService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<ProducerService> _logger;

    public ProducerService(IPublishEndpoint publishEndpoint, ILogger<ProducerService> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var evt = new StageOneEvent
        {
            CorrelationId = Guid.NewGuid(),
            Name = "Hello World",
            Id = Random.Shared.Next()
        };

        _logger.LogWarning("Publishing StageOneEvent with CorrelationId: {EvtCorrelationId}", evt.CorrelationId);
        
        await _publishEndpoint.Publish(evt, cancellationToken);
    }
}
