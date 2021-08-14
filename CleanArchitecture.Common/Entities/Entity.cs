namespace CleanArchitecture.Common.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
