namespace CleanArchitecture.Common.Entities
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
