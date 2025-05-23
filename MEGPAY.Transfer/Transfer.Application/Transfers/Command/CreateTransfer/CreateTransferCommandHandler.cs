using System.Net;
using MediatR;
using Transfer.Domain.SeedWork;
using Transfer.Domain.TransferAggregate;
using Transfer.Shared.Models;
using Transfer.Application.Transfers.Interfaces;
using Transfer.Shared.Events;

namespace Transfer.Application.Transfers.Command.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Response<bool>>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDbContextHandler _dbContextHandler;
    private readonly ITransferEventPublisher _eventPublisher;

    public CreateTransferCommandHandler(
        ITransferRepository transferRepository,
        IDbContextHandler dbContextHandler,
        ITransferEventPublisher eventPublisher)
    {
        _transferRepository = transferRepository;
        _dbContextHandler = dbContextHandler;
        _eventPublisher = eventPublisher;
    }

    public async Task<Response<bool>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = new Domain.TransferAggregate.Transfer(Guid.NewGuid(), request.FromAccountId, request.ToAccountId, request.Amount);
        await _transferRepository.SaveAsync(transfer, cancellationToken);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        var initiatedEvent = new TransferInitiatedEvent
        {
            TransferId = transfer.Id,
            FromAccountId = transfer.FromAccountId,
            ToAccountId = transfer.ToAccountId,
            Amount = (decimal)transfer.Amount
        };
        await _eventPublisher.PublishAsync(initiatedEvent);
        var completedEvent = new TransferCompletedEvent
        {
            TransferId = transfer.Id,
            ToAccountId = transfer.ToAccountId,
            Amount = (decimal)transfer.Amount
        };
        await _eventPublisher.PublishAsync(completedEvent);
        return Response<bool>.Success(true, HttpStatusCode.Created);
    }
}