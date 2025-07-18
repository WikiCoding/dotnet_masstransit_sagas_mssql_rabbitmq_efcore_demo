namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;

public class StageThreeEvent
{
    public Guid CorrelationId { get; set; }
    public string Result { get; set; } = string.Empty;
}
