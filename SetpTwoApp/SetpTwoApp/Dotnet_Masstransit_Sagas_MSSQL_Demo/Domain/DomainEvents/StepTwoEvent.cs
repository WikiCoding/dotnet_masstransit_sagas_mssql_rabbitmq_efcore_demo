namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;

public class StageTwoEvent
{
    public string Description { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; }
}

