using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hahn.ApplicationProcess.December2020.Data.Infrastructure
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public Repository(DbContext context)
            : base(context)
        {
        }

        public virtual TEntity Create(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity).Entity;
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                if (Context.Set<TEntity>().Local.All(e => e.Id.ToString() != entity.Id.ToString()))
                {
                    Context.Set<TEntity>().Attach(entity);
                }

                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                // do nothing
            }
        }


        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            
            if (Context.Set<TEntity>().Local.All(e => e.Id.ToString() != entity.Id.ToString()))
            {
                Context.Set<TEntity>().Attach(entity);
            }
            EntityEntry<TEntity> entry = Context.Entry(entity);
            foreach (var selector in properties)
            {
                entry.Property(selector).IsModified = true;
            }
        }

        public virtual void Delete(object id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void DeleteLogically(object id)
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            if (entity == null) return;
            entity.EntityState = entity.EntityState | ApplicationEntityState.IsDeleted;
            Update(entity);
        }

        public void DeleteLogically(TEntity entity)
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                entity.EntityState = entity.EntityState | ApplicationEntityState.IsDeleted;
                dbSet.Attach(entity);
            }
            Update(entity);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }
    }
}