# Some Rules

*Notes on using a MassTransit Saga State Machine with EF Core.*

1. `CorrelationId` is mandatory in the various models.
2. The `StateMachine` class must implement `MassTransitStateMachine<TSagaStateMachineInstance>` and does **not** allow injecting `DbContext` in the constructor, so you need to create an `Activity` to persist things.
3. An `Activity` must be marked with `IStateMachineActivity<TSagaStateMachineInstance, TEventThatTriggersIt>`.
4. Because of point 2, the `StateMachine` only tracks the class marked as `SagaStateMachineInstance`, which must have the `CorrelationId` property and updates its state when `TransitionTo(...)` is called. This update is done in memory and the transaction is completed just at the end of the pipeline if there were no errors along the way.
5. It's mandatory to create an `ISagaClassMap` that represents the `SagaStateMachineInstance` model, but it's not required to map the `CorrelationId`.
6. The `DbContext` must implement `SagaDbContext`, and therefore in the override of the `Configure` method, you must return the instance of the `ISagaClassMap`.
7. Since everything has a `CorrelationId` and I'm publishing new events of the same type that are registered as `Event<out TMessage>`, the saga works until the end. Anything outside of that requires creating a separate consumer—it must consume the published message, do what it needs to do, and publish a new event that aligns with the next step of the Saga.
8. MassTransit creates fanout Exchanges for the different steps of the saga. If by any reason there's another consumer for that Exchange, this error occours:
```log
fail: MassTransit.ReceiveTransport[0]
      R-FAULT rabbitmq://localhost/stages-saga-model 346a0000-1db7-a42e-9cd3-08ddc6f6f532 Dotnet_Masstransit_Sagas_MSSQL_Demo.Domain.DomainEvents.StageThreeEvent Dotnet_Masstransit_Sagas_MSSQL_Demo.Sagas.Persistence.DataModels.StagesSagaModel(00:00:00.8298275)
```
9. If you want an external consumer, you can do so and your pipeline will look like this:
```csharp
Initially(
    When(StageOneEvent).Then(context =>
    {
        _logger.LogWarning("Kicking off StageOne and Transitioning to StageTwo with {}", context.Saga.CorrelationId);
        context.Saga.CorrelationId = context.Message.CorrelationId;
        context.Saga.Id = context.Message.Id;
    }).TransitionTo(StageThree) // instead of StageTwo
    .Activity(selector => selector.OfType<StageOneActivity>()) // added so it's possible to inject the dbContext and persist StageOneModel
    .Publish(context => new StageTwoEvent
    {
        CorrelationId = context.Message.CorrelationId,
        Description = "Yooo"
    }));

//During(StageTwo, When(StageTwoEvent).Then(context =>
//{
//    _logger.LogWarning("Kicking off StageTwo and Transitioning to StageThree with {}", context.Saga.CorrelationId);
//    context.Saga.CorrelationId = context.Message.CorrelationId;
//}).TransitionTo(StageThree)
//.Publish(context => new StageThreeEvent
//{
//    CorrelationId = context.Message.CorrelationId,
//    Result = "Funcionou bem!"
//})
//);

During(StageThree, When(StageThreeEvent).Then(context =>
{
    _logger.LogWarning("Completing saga with {}", context.Saga.CorrelationId);
}).TransitionTo(Final));
```

# Running the app
1. In the root dir open the terminal and `docker-compose up -d`
2. Start the app normally in `Development` so Migrations will be automatically applied
3. Then just http request or curl `curl http://localhost:5000/sagas`