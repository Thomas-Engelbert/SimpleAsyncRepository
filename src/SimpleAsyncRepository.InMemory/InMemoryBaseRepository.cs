using System.Collections.Concurrent;
using SimpleAsyncRepository.Abstractions;

namespace SimpleAsyncRepository.InMemory;

public class InMemoryBaseRepository<T> : IRepository<T> where T : class, IModel
{
    private readonly ConcurrentDictionary<Guid, T> _dictionary = new();

    public Task<int> Count() => Task.FromResult(_dictionary.Count);

    public Task<IList<T>> GetAll() => Task.FromResult((IList<T>)[.. _dictionary.Values]);

    public Task<T?> GetById(Guid id)
    {
        _ = _dictionary.TryGetValue(id, out T? item);
        return Task.FromResult(item);
    }

    public Task RemoveById(Guid id)
    {
        _ = _dictionary.TryRemove(id, out T? _);
        return Task.CompletedTask;
    }

    public Task Upsert(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        _ = _dictionary.AddOrUpdate(entity.Id, entity, (guid, oldState) => entity);
        return Task.CompletedTask;
    }
}
