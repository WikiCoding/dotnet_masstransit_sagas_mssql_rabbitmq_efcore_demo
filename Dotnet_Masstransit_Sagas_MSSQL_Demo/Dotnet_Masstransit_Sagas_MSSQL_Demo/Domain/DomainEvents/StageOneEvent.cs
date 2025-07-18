using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;

public class StageOneEvent : CorrelatedBy<Guid>
{
    public string Id { get; set; } = DateTime.UtcNow.ToString();
    public string Name { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
}
