using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.Models;
using Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Activities;
using MassTransit;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas;

public class StagesStateMachine : MassTransitStateMachine<StagesSagaModel>
{
    private readonly ILogger<SagaStateMachineInstance> _logger;

    public StagesStateMachine(ILogger<SagaStateMachineInstance> logger)
    {
        _logger = logger;

        SetSaga();
    }

    private void SetSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => StageOneEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => StageTwoEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => StageThreeEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(StageOneEvent).Then(context =>
            {
                _logger.LogWarning("Kicking off StageOne and Transitioning to StageTwo with {}", context.Saga.CorrelationId);
                context.Saga.CorrelationId = context.Message.CorrelationId;
            }).TransitionTo(StageTwo)
            .Activity(selector => selector.OfType<StageOneActivity>()) // added so it's possible to inject the dbContext and persist StageOneModel
            .Publish(context => new StageTwoEvent
            {
                CorrelationId = context.Message.CorrelationId,
                Description = "Yooo"
            }));

        During(StageTwo, When(StageTwoEvent).Then(context =>
        {
            _logger.LogWarning("Kicking off StageTwo and Transitioning to StageThree with {}", context.Saga.CorrelationId);
            context.Saga.CorrelationId = context.Message.CorrelationId;
        }).TransitionTo(StageThree)
        .Publish(context => new StageThreeEvent
        {
            CorrelationId = context.Message.CorrelationId,
            Result = "Funcionou bem!"
        })
        );

        During(StageThree, When(StageThreeEvent).Then(context =>
        {
            _logger.LogWarning("Completing saga with {}", context.Saga.CorrelationId);
        }).TransitionTo(Final));
    }

    // States
    public State StageOne { get; private set; }
    public State StageTwo { get; private set; }
    public State StageThree { get; private set; }
    public State Final { get; private set; }

    // Events
    public Event<StageOneEvent> StageOneEvent { get; private set; }
    public Event<StageTwoEvent> StageTwoEvent { get; private set; }
    public Event<StageThreeEvent> StageThreeEvent { get; private set; }
}
