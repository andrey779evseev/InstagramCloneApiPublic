using Domain.Models.EndpointLog;

namespace Domain.Interfaces.Repositories;

public interface IEndpointLogRepository
{
    public Task Save(EndpointLog log);
}