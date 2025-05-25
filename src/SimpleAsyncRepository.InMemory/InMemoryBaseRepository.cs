namespace SimpleAsyncRepository.InMemory;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleAsyncRepository.Abstractions;

public class InMemoryBaseRepository<T> : IRepository<T> where T : class, IModel
{
    private readonly ConcurrentDictionary<Guid, T> dictionary = new ();

    public Task<int> Count () => Task.FromResult ( this.dictionary.Count );

    public Task<IList<T>> GetAll () => Task.FromResult ( (IList<T>) this.dictionary.Values.ToList () );

    public Task<T?> GetById ( Guid id )
    {
        _ = this.dictionary.TryGetValue ( id, out T? item );
        return Task.FromResult ( item );
    }

    public Task RemoveById ( Guid id )
    {
        _ = this.dictionary.TryRemove ( id, out T? _ );
        return Task.CompletedTask;
    }

    public Task Upsert ( T entity )
    {
        if ( entity.Id == Guid.Empty )
        {
            entity.Id = Guid.NewGuid ();
        }

        _ = this.dictionary.AddOrUpdate ( entity.Id, entity, ( guid, oldState ) => entity );
        return Task.CompletedTask;
    }
}
