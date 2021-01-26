using System;

namespace Hahn.ApplicationProcess.December2020.Domain.Infrastructure
{
    public abstract class Entity: Entity<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }

    public class Entity<TPrimaryKey> :BaseEntity, IEntity
    {
        public virtual TPrimaryKey Id { get; set; }

        object IEntity.Id
        {
            get => this.Id;
            set
            {
                if (value is TPrimaryKey key)
                    Id = key;
            }
        }
    }
}