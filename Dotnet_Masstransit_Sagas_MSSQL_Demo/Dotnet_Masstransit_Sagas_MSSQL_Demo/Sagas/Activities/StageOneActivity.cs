using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.DataModels;
using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Activities;

public class StageOneActivity : IStateMachineActivity<StagesSagaModel, StageOneEvent>
{
    private readonly ILogger<StageOneActivity> _logger;
    private readonly StagesDbContext _dbContext;

    public StageOneActivity(ILogger<StageOneActivity> logger, StagesDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Execute(BehaviorContext<StagesSagaModel, StageOneEvent> context, IBehavior<StagesSagaModel, StageOneEvent> next)
    {
        var stageOneModel = new StageOneModel
        {
            Id = context.Message.Id,
            Name = context.Message.Name, // getting it from the message!
            CorrelationId = context.Saga.CorrelationId
        };
        
        _dbContext.StageOneModel.Add(stageOneModel);
        await _dbContext.SaveChangesAsync();
        _logger.LogWarning("Persisted Stage One Model from Saga with CorrelationId {}", context.Saga.CorrelationId);
        
        await next.Execute(context).ConfigureAwait(false);
    }
    
    public void Probe(ProbeContext context)
    {
        context.CreateScope(GetType().Name);
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<StagesSagaModel, StageOneEvent, TException> context, IBehavior<StagesSagaModel, StageOneEvent> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}