namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.DataModels;

public class StageTwoModel
{
    public string Description { get; set; } = string.Empty;
    public Guid CorrelationId { get; set; }
}
