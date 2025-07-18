using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.StateMaps;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.DbContext;

public class StagesDbContext : SagaDbContext
{
    public StagesDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new StagesSagaClassMap();
            yield return new StageOneClassMap();
            yield return new StageTwoClassMap();
            yield return new StageThreeClassMap();
        }
    }
}
