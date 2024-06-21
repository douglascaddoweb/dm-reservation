namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IRepositoryGeneric<TEntity, TId>
        where TEntity : class
        where TId : struct
    {

        Task AddAsync(TEntity entity);
        Task<TEntity> FindIdAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        Task<bool> CommitAsync();
        void Remove(TEntity entity);
    }
}
