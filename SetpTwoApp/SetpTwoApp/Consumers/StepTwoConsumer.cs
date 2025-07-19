using MassTransit;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;

namespace SetpTwoApp.Consumers;

public class StepTwoConsumer : IConsumer<StageTwoEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<StepTwoConsumer> _logger;

    public StepTwoConsumer(IPublishEndpoint publishEndpoint, ILogger<StepTwoConsumer> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<StageTwoEvent> context)
    {
        _logger.LogWarning("Kicked off the external consumer...");
        //await Task.Delay(TimeSpan.FromSeconds(20));

        var stageThreeEvent = new StageThreeEvent
        {
            CorrelationId = context.Message.CorrelationId,
            Result = "Funcionou bem!"
        };

        _logger.LogWarning("Publishing StageThree Event with CorrelationId {}", context.Message.CorrelationId);

        await _publishEndpoint.Publish(stageThreeEvent);

        await Task.CompletedTask;
    }
}
