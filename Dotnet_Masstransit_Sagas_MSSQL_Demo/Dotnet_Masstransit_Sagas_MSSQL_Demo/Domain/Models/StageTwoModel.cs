using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.Models;

public class StageTwoModel : SagaStateMachineInstance
{
    public string Description { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; }
}
