namespace Husa.Extensions.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Domain.Extensions;

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

        public void CopyProperties(object target, IEnumerable<string> include) => target.CopyProperties(include);

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
