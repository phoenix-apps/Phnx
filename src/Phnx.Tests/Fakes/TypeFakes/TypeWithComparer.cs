using System;

namespace Phnx.Tests.Fakes.TypeFakes
{
    internal class TypeWithComparer : IComparable<TypeWithComparer>, IEquatable<TypeWithComparer>
    {
        public TypeWithComparer(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public int CompareTo(TypeWithComparer obj)
        {
            return Value.CompareTo(obj.Value);
        }

        public bool Equals(TypeWithComparer other)
        {
            return Value.Equals(other.Value);
        }
    }
}
