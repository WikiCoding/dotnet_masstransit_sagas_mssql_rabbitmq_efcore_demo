using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.DataModels;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.StateMaps;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence;

public class StagesDbContext : SagaDbContext
{
    public StagesDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<StageOneModel> StageOneModel { get; set; }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new StagesSagaClassMap();
        }
    }
}
