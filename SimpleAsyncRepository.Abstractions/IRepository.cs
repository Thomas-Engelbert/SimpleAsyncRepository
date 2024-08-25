namespace SimpleAsyncRepository.Abstractions;

public interface IRepository<T> where T : class, IModel
{
    Task<T?> GetById ( Guid id );
    Task<IList<T>> GetAll ();
    Task Upsert ( T entity );
    Task RemoveById ( Guid id );
}
