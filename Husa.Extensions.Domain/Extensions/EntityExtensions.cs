namespace Husa.Extensions.Domain.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Husa.Extensions.Domain.Interfaces;

    public static class EntityExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Perform changes in IEntity without expose sensible fields as public")]
        private static readonly BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public static void ActivateExt(this IEntity entity)
        {
            if (!entity.IsDeleted)
            {
                return;
            }

            entity.GetType().GetProperty("IsDeleted", BindingFlags).SetValue(entity,  false);
        }

        public static void DeleteExt(this IEntity entity, Guid userId, bool deleteChildren = false)
        {
            if (entity.IsDeleted)
            {
            return;
        }

            if (deleteChildren)
            {
                entity.DeleteChildren(userId);
            }

            entity.GetType().GetProperty("IsDeleted", BindingFlags).SetValue(entity, true);
        }

        public static bool IsEqualToExt(this IEntity entity, object obj)
        {
            if (obj == null || obj.GetType() != entity.GetType())
            {
                return false;
            }

            var other = (IEntity)obj;

            return entity.GetEntityEqualityComponents()
                .SequenceEqual(other.GetEntityEqualityComponents());
        }

        public static void UpdateTrackValuesExt(this IEntity entity, Guid userId, bool isNewRecord = false)
        {
            var type = entity.GetType();
            type.GetProperty("SysTimestamp", BindingFlags).SetValue(entity, DateTime.UtcNow);
            if (isNewRecord)
            {
                type.GetProperty("SysCreatedBy", BindingFlags).SetValue(entity, userId);
            }
            else
            {
                type.GetProperty("SysModifiedBy", BindingFlags).SetValue(entity, userId);
                type.GetProperty("SysModifiedOn", BindingFlags).SetValue(entity, DateTime.UtcNow);
            }
        }

        private static IEnumerable<object> GetEntityEqualityComponents(this IEntity entity)
        {
            var methodInfo = entity.GetType()
                .GetMethod("GetEntityEqualityComponents", BindingFlags);
            return (IEnumerable<object>)methodInfo.Invoke(entity, null);
        }

        private static void DeleteChildren(this IEntity entity, Guid userId)
        {
            var methodInfo = entity.GetType()
                .GetMethod("DeleteChildren", BindingFlags);
            methodInfo.Invoke(entity, new object[] { userId });
        }
    }
}
