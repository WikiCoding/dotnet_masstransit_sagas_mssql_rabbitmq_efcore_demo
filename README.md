# Some Rules

*Notes on using a MassTransit Saga State Machine with EF Core.*

1. `CorrelationId` is mandatory in the various models.
2. The `StateMachine` class must implement `MassTransitStateMachine<TSagaStateMachineInstance>` and does **not** allow injecting `DbContext` in the constructor, so you need to create an `Activity` to persist things.
3. An `Activity` must be marked with `IStateMachineActivity<TSagaStateMachineInstance, TEventThatTriggersIt>`.
4. Because of point 2, the `StateMachine` only tracks the class marked as `SagaStateMachineInstance`, which must have the `CorrelationId` property and updates its state when `TransitionTo(...)` is called. This update is done in memory and the transaction is completed just at the end of the pipeline if there were no errors along the way.
5. It's mandatory to create an `ISagaClassMap` that represents the `SagaStateMachineInstance` model, but it's not required to map the `CorrelationId`.
6. The `DbContext` must implement `SagaDbContext`, and therefore in the override of the `Configure` method, you must return the instance of the `ISagaClassMap`.
7. Since everything has a `CorrelationId` and I'm publishing new events of the same type that are registered as `Event<out TMessage>`, the saga works until the end. Anything outside of that requires creating a separate consumer—it must consume the published message, do what it needs to do, and publish a new event that aligns with the next step of the Saga.