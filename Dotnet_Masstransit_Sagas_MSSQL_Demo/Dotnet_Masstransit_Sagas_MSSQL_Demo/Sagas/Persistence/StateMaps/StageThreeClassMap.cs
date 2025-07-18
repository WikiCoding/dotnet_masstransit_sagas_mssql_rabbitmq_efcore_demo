using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.StateMaps;

public class StageThreeClassMap : SagaClassMap<StageThreeModel>
{
    protected override void Configure(EntityTypeBuilder<StageThreeModel> entity, ModelBuilder model)
    {
        base.Configure(entity, model);

        entity.Property(p => p.Result);
    }
}
