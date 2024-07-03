using Domain;

namespace Services.Repositories.Abstractions;

public interface IStatusRepository
{
    public Task<Status> GetAsync(Guid id);

    public Task<Status> GetByOrderIdAsync(Status entity);

    public Task<Status> UpdateAsync(Status entity);
}