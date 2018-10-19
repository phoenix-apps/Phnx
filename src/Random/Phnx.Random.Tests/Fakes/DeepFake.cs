using System;

namespace Phnx.Random.Tests.Fakes
{
    public class DeepFake
    {
        public uint Id { get; set; }

        public ShallowFake Single { get; set; }

        public ShallowFake[] Collection { get; set; }

        public RecursiveFake Recursive { get; set; }
    }

    public class ShallowFake
    {
        public string RandomText { get; set; }

        public int VersionId { get; set; }

        public Guid Id { get; set; }

        public string IdAndVersionId => Id + ": " + VersionId;
    }

    public class RecursiveFake
    {
        public RecursiveFake Self { get; set; }
    }
}
