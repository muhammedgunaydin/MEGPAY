using Account.Application.Interfaces;
using Account.Domain.Events;
using Account.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Account.Infrastructure.Services;

public class AccountEventStore : IAccountEventStore
{
    private readonly AppDbContext _context;

    public AccountEventStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task AppendEventAsync(AccountEvent @event, CancellationToken cancellationToken)
    {
        var existingEvents = _context.AccountEvents
            .Where(x => x.AccountId == @event.AccountId);

        int version = await existingEvents.CountAsync(cancellationToken);

        var serialized = JsonConvert.SerializeObject(@event);
        var entity = new AccountEventEntity
        {
            Id = Guid.NewGuid(),
            AccountId = @event.AccountId,
            Version = version + 1,
            EventType = @event.GetType().Name,
            Data = serialized
        };

        _context.AccountEvents.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<AccountEvent>> GetEventsAsync(Guid accountId)
    {
        var eventEntities = await _context.AccountEvents
            .Where(x => x.AccountId == accountId)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var result = new List<AccountEvent>();

        foreach (var e in eventEntities)
        {
            var type = Type.GetType($"Account.Domain.Events.{e.EventType}");
            if (type == null) continue;

            var deserialized = (AccountEvent)JsonConvert.DeserializeObject(e.Data, type)!;
            result.Add(deserialized);
        }

        return result;
    }
}
