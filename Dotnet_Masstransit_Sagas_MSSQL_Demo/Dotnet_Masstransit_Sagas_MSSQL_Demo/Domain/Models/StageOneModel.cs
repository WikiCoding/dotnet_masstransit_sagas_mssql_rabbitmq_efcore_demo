using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.Models;

public class StageOneModel : SagaStateMachineInstance
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; }
}
