namespace Transfer.Application.Interfaces;

public interface ITransferStatusService
{
    Task UpdateStatusAsync(Guid transferId, string status);
    Task CreateAsync(Guid transferId, string status);
}