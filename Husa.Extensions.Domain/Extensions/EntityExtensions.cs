namespace Husa.Extensions.Domain.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Husa.Extensions.Domain.Interfaces;

    public static class EntityExtensions
    {
        [SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "This will be the only one way to set following fields")]
        public static void UpdateTrackValuesExt(this IEntity entity, Guid userId, bool isNewRecord = false)
        {
            var type = entity.GetType();
            var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            type.GetProperty("SysTimestamp", flags).SetValue(entity, DateTime.UtcNow);
            if (isNewRecord)
            {
                type.GetProperty("SysCreatedBy", flags).SetValue(entity, userId);
            }
            else
            {
                type.GetProperty("SysModifiedBy", flags).SetValue(entity, userId);
                type.GetProperty("SysModifiedOn", flags).SetValue(entity, DateTime.UtcNow);
            }
        }
    }
}
