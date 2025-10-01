namespace Husa.Extensions.Domain.Interfaces
{
    using System;

    public interface IEntity
    {
        Guid Id { get; }

        DateTime SysCreatedOn { get; }

        Guid? SysCreatedBy { get; }

        Guid? SysModifiedBy { get; }

        DateTime? SysModifiedOn { get; }

        DateTime SysTimestamp { get; }

        bool IsDeleted { get; }

        void UpdateTrackValues(Guid userId, bool isNewRecord = false);

        void Delete(Guid userId, bool deleteChildren = false);

        bool IsEqualTo(object obj);

        void Activate();
    }
}
