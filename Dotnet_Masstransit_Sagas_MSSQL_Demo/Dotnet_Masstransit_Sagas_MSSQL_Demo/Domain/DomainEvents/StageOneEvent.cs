namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;

public class StageOneEvent
{
    public int Id { get; set; } = Random.Shared.Next();
    public string Name { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
}
