namespace SimpleAsyncRepository.InMemory;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleAsyncRepository.Abstractions;

public class InMemoryBaseRepository<T> : IRepository<T> where T : class, IModel
{
    public Task<int> Count () => throw new NotImplementedException ();
    public Task<IList<T>> GetAll () => throw new NotImplementedException ();
    public Task<T?> GetById ( Guid id ) => throw new NotImplementedException ();
    public Task RemoveById ( Guid id ) => throw new NotImplementedException ();
    public Task Upsert ( T entity ) => throw new NotImplementedException ();
}
