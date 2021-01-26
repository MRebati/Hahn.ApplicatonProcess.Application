using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Hahn.ApplicationProcess.December2020.Domain.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {
        private bool _disposed;

        private TransactionScope _transaction;
        private TransactionOptions _transactionOptions;
        protected DbContext Context;

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        
        public virtual void Save()
        {
            try
            {
                //var entities = from e in ChangeTracker.Entries()
                //    where e.State == EntityState.Added
                //          || e.State == EntityState.Modified
                //    select e.Entity;
                //foreach (var entity in entities)
                //{
                //    var validationContext = new ValidationContext(entity);
                //    Validator.ValidateObject(entity, validationContext);
                //}

                Context.SaveChanges();
            }
            catch (ValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return Context.SaveChangesAsync();
            }
            catch (ValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(ValidationException e)
        {
            //var errorMessages = e.ValidationResult.ErrorMessage;

            //var fullErrorMessage = string.Join("; ", errorMessages);
            //var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw e;
            //throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }

        /// <summary>
        /// creates new instance of transaction scope.
        /// </summary>
        /// <returns></returns>
        public virtual TransactionScope Begin()
        {
            _transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
            _transactionOptions.Timeout = AppConst.TransactionTimeout;
            _transaction = new TransactionScope(TransactionScopeOption.Required,_transactionOptions,TransactionScopeAsyncFlowOption.Enabled);
            
            return _transaction;
        }

        /// <summary>
        /// indicates that all operations in transaction 
        /// within scope are completed successfully.
        /// </summary>
        /// <returns></returns>
        public virtual Task CompleteAsync()
        {
            return Task.Run(() => Complete());
        }

        /// <summary>
        /// indicates that all operations in transaction 
        /// within scope are completed successfully.
        /// </summary>
        /// <returns></returns>
        public virtual void Complete()
        {
            _transaction.Complete();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _transaction?.Dispose();
                Context?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}