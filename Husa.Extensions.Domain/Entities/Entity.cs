namespace Husa.Extensions.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Husa.Extensions.Domain.Extensions;
    using Husa.Extensions.Domain.Interfaces;

    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            this.SysCreatedOn = DateTime.UtcNow;
            this.SysTimestamp = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [Key]
        public virtual Guid Id { get; set; }

        public virtual DateTime SysCreatedOn { get; protected set; }

        public virtual DateTime? SysModifiedOn { get; protected set; }

        public virtual bool IsDeleted { get; protected set; }

        public virtual Guid? SysModifiedBy { get; protected set; }

        public virtual Guid? SysCreatedBy { get; protected set; }

        public virtual DateTime SysTimestamp { get; protected set; }

        public virtual Guid CompanyId { get; set; }

        public virtual void Activate()
        {
            if (!this.IsDeleted)
            {
                return;
            }

            this.IsDeleted = false;
        }

        public virtual void Delete(Guid userId, bool deleteChildren = false)
            => this.DeleteExt(userId, deleteChildren);

        public bool IsEqualTo(object obj) => this.IsEqualToExt(obj);

        public virtual void UpdateTrackValues(Guid userId, bool isNewRecord = false)
            => this.UpdateTrackValuesExt(userId, isNewRecord);

        protected abstract void DeleteChildren(Guid userId);

        protected abstract IEnumerable<object> GetEntityEqualityComponents();
    }
}
