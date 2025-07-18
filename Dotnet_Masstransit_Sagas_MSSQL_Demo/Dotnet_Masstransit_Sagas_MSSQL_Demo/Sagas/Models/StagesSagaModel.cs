using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;

public class StagesSagaModel : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public int id { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public string CurrentState { get; set; } = string.Empty;
    public byte[] RowVersion { get; set; }
}
