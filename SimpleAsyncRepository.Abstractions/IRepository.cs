namespace SimpleAsyncRepository.Abstractions;

public interface IRepository<T> where T : class, IModel
{
    Task<T?> GetById ( Guid id );

    Task<IList<T>> GetAll ();

    /// <summary>
    /// Updates an entity if it exists, or inserts it if it doesn't exist.
    /// </summary>
    /// <remarks>
    /// If the entity's has a Guid that is used by another entity that is already stored,
    /// the stored entity will be updated. Otherwise, the entity will just be inserted.
    /// If the Guid of the entity is Guid.Empty, then the entity will receive a random guid.
    /// </remarks>
    Task Upsert ( T entity );

    Task RemoveById ( Guid id );
}
