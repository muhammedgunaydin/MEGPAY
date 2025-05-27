using System.Net;
using MediatR;
using MassTransit;
using Microsoft.Extensions.Logging;
using Transfer.Domain.SeedWork;
using Transfer.Domain.TransferAggregate;
using Transfer.Shared.Events;
using Transfer.Application.Interfaces;

namespace Transfer.Application.Transfers.Command.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Shared.Models.Response<bool>>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDbContextHandler _dbContextHandler;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ITransferStatusService _statusService;
    private readonly ILogger<CreateTransferCommandHandler> _logger;

    public CreateTransferCommandHandler(
        ITransferRepository transferRepository,
        IDbContextHandler dbContextHandler,
        IPublishEndpoint publishEndpoint,
        ITransferStatusService statusService,
        ILogger<CreateTransferCommandHandler> logger)
    {
        _transferRepository = transferRepository;
        _dbContextHandler = dbContextHandler;
        _publishEndpoint = publishEndpoint;
        _statusService = statusService;
        _logger = logger;
    }

    public async Task<Shared.Models.Response<bool>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var transferId = Guid.NewGuid();
        var transfer = new Domain.TransferAggregate.Transfer(transferId, request.FromAccountId, request.ToAccountId, request.Amount);
        
        await _transferRepository.SaveAsync(transfer, cancellationToken);
        await _statusService.CreateAsync(transferId, "Pending");
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("CreateTransferCommand received → From: {From}, To: {To}, Amount: {Amount}",
            request.FromAccountId, request.ToAccountId, request.Amount);


        await _publishEndpoint.Publish(new TransferStartedEvent
        {
            TransferId = transfer.Id,
            FromAccountId = transfer.FromAccountId,
            ToAccountId = transfer.ToAccountId, 
            Amount = transfer.Amount,          
            ClientRequestId = request.ClientRequestId
        }, cancellationToken);

        return Shared.Models.Response<bool>.Success(true, HttpStatusCode.Created);
    }
}