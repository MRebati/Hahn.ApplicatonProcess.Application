using System;
using Hahn.ApplicationProcess.December2020.Domain.Types;

namespace Hahn.ApplicationProcess.December2020.Domain.Infrastructure
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDateTime = DateTime.Now;
        }

        public string CreatorUserId { get; set; }

        public string UpdateUserId { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public DateTime CreateDateTime { get; private set; }

        public ApplicationEntityState EntityState { get; set; }
    }
}