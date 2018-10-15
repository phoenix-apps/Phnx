using System;

namespace Phnx.IO.Json.Tests.Fakes
{
    public class ShallowFake : IEquatable<ShallowFake>
    {
        public int Id { get; set; }

        public string[] Collection { get; set; }

        public override bool Equals(object other)
        {
            if (!(other is ShallowFake sOther)) return false;
            return Id == sOther.Id;
        }

        public bool Equals(ShallowFake other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
