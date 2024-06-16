namespace DMReservation.Domain.Entities
{
    public abstract class Entity<TEntity, TId>
        where TEntity : class
        where TId : struct
    {

        public TId Id { get; protected set; }
    }
}
