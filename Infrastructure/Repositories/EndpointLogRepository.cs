using Domain.Interfaces.Repositories;
using Domain.Models.EndpointLog;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class EndpointLogRepository : IEndpointLogRepository
{
    private readonly SupabaseDbContext _ctx;

    public EndpointLogRepository(SupabaseDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task Save(EndpointLog log)
    {
        await _ctx.EndpointLogs.AddAsync(log);
        await _ctx.SaveChangesAsync();
    }
}