namespace Husa.Extensions.Common.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public abstract class ValueObject : IComparable
    {
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return NotEqualOperator(left, right);
        }

        public static bool operator <(ValueObject left, ValueObject right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(ValueObject left, ValueObject right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(ValueObject left, ValueObject right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(ValueObject left, ValueObject right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

        public void CopyProperties(object target, IEnumerable<string> include)
        {
            if (target == null || include == null || !include.Any())
            {
                return;
            }

            Type sourceType = this.GetType();
            Type targetType = target.GetType();
            var sourceProperties = sourceType.GetProperties().Where(p => include.Contains(p.Name));
            foreach (var property in sourceProperties)
            {
                var targetProperty = targetType.GetProperty(property.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var sourceValue = property.GetValue(this);
                    targetProperty.SetValue(target, sourceValue, null);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            if (GetUnproxiedType(this) != GetUnproxiedType(obj))
            {
                return false;
            }

            var valueObject = (ValueObject)obj;
            return this.GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return this.GetEqualityComponents().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
        }

        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }

        public virtual int CompareTo(ValueObject other)
        {
            if (other is null)
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            Type thisType = GetUnproxiedType(this);

            Type otherType = GetUnproxiedType(other);

            if (thisType != otherType)
            {
                return string.Compare($"{thisType}", $"{otherType}", StringComparison.Ordinal);
            }

            return this.GetEqualityComponents().Zip(
                other.GetEqualityComponents(), (left, rigth) => (
                (IComparable)left)?.CompareTo(rigth) ?? (rigth is null ? 0 : -1))
                .FirstOrDefault(cmp => cmp != 0);
        }

        public virtual int CompareTo(object obj)
        {
            return this.CompareTo(obj as ValueObject);
        }

        internal static Type GetUnproxiedType(object obj)
        {
            const string EFCoreProxyPrefix = "Castle.Proxies.";
            const string NHibernateProxyPostfix = "Proxy";
            Type type = obj.GetType();
            string typeString = type.ToString();
            if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
            {
                return type.BaseType;
            }

            return type;
        }

        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
