using MassTransit;
using Transfer.Shared.Events;

namespace Transfer.Infrastructure.Saga
{
    public class TransferStateMachine : MassTransitStateMachine<TransferState>
    {
        public State Pending { get; private set; } = null!;
        public State Completed { get; private set; } = null!;
        public State Failed { get; private set; } = null!;

        public Event<TransferStartedEvent> TransferStarted { get; private set; } = null!;
        public Event<TransferCompletedEvent> TransferCompleted { get; private set; } = null!;
        public Event<TransferFailedEvent> TransferFailed { get; private set; } = null!;

        public TransferStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => TransferStarted, x =>
            {
                x.CorrelateById(context => context.Message.TransferId);
                x.InsertOnInitial = true;
                x.SetSagaFactory(context => new TransferState
                {
                    CorrelationId = context.Message.TransferId,
                    FromAccountId = context.Message.FromAccountId,
                    ToAccountId = context.Message.ToAccountId,
                    Amount = context.Message.Amount,
                    ClientRequestId = context.Message.ClientRequestId,
                    CreatedAt = DateTime.UtcNow
                });
            });

            Event(() => TransferCompleted, x =>
            {
                x.CorrelateById(context => context.Message.TransferId);
            });

            Event(() => TransferFailed, x =>
            {
                x.CorrelateById(context => context.Message.TransferId);
            });

            Initially(
                When(TransferStarted)
                    .TransitionTo(Pending)
                    .Then(context =>
                    {
                        Console.WriteLine($"[Saga] Transfer STARTED → {context.Saga.CorrelationId}");
                    })
            );

            During(Pending,
                When(TransferCompleted)
                    .Then(context =>
                    {
                        context.Saga.CompletedAt = DateTime.UtcNow;
                        Console.WriteLine($"[Saga] Transfer COMPLETED → {context.Saga.CorrelationId}");
                    })
                    .TransitionTo(Completed),

                When(TransferFailed)
                    .Then(context =>
                    {
                        context.Saga.FailedAt = DateTime.UtcNow;
                        Console.WriteLine($"[Saga] Transfer FAILED → {context.Saga.CorrelationId}");
                    })
                    .TransitionTo(Failed)
            );
        }
    }
}
