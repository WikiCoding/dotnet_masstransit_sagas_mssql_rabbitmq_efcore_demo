using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.StateMaps;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.StateMaps;

public class StagesSagaClassMap : SagaClassMap<StagesSagaModel>
{
    protected override void Configure(EntityTypeBuilder<StagesSagaModel> entity, ModelBuilder model)
    {
        base.Configure(entity, model);

        entity.Property(p => p.StartDate);
        entity.Property(p => p.CurrentState);
        entity.Property(p => p.id);
        entity.Property(p => p.RowVersion).IsRowVersion();
    }
}
