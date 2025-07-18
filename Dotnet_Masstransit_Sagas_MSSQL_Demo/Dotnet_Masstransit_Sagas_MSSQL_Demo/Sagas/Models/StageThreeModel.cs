using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;

public class StageThreeModel: SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string Result { get; set; } = string.Empty;
}
