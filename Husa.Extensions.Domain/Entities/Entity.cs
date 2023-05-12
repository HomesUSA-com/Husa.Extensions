namespace Husa.Extensions.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class Entity
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

        public virtual Guid? SysModifiedBy { get; set; }

        public virtual Guid? SysCreatedBy { get; set; }

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
        {
            if (this.IsDeleted)
            {
                return;
            }

            if (deleteChildren)
            {
                this.DeleteChildren(userId);
            }

            this.IsDeleted = true;
        }

        public virtual void UpdateTrackValues(Guid userId, bool isNewRecord = false)
        {
            if (isNewRecord)
            {
                this.SysCreatedBy = userId;
            }

            this.SysModifiedOn = DateTime.UtcNow;
            this.SysTimestamp = DateTime.UtcNow;
            this.SysModifiedBy = userId;
        }

        public bool IsEqualTo(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            var other = (Entity)obj;

            return this.GetEntityEqualityComponents().SequenceEqual(other.GetEntityEqualityComponents());
        }

        protected abstract void DeleteChildren(Guid userId);

        protected abstract IEnumerable<object> GetEntityEqualityComponents();
    }
}
