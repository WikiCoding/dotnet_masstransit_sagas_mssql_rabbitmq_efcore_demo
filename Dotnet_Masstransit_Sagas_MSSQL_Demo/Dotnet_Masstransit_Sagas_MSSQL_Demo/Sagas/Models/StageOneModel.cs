using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;

public class StageOneModel : SagaStateMachineInstance
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; }
}
