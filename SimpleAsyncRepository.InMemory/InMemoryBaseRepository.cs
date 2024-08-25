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

    public Task<IList<T>> GetAll () => throw new NotImplementedException ();
    public Task<T?> GetById ( Guid id ) => throw new NotImplementedException ();
    public Task RemoveById ( Guid id ) => throw new NotImplementedException ();
    public Task Upsert ( T entity ) => throw new NotImplementedException ();
}
