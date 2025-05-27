using System.ComponentModel.DataAnnotations;
using MassTransit;

namespace Transfer.Infrastructure.Saga
{
    public class TransferState : SagaStateMachineInstance
    {
        [Key]
        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; } = string.Empty;

        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }

        public string ClientRequestId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? FailedAt { get; set; }
    }
}