using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.StateMaps;

public class StageOneClassMap : SagaClassMap<StageOneModel>
{
    protected override void Configure(EntityTypeBuilder<StageOneModel> entity, ModelBuilder model)
    {
        base.Configure(entity, model);

        entity.Property(p => p.Id);
        entity.Property(p => p.Name);
    }
}
