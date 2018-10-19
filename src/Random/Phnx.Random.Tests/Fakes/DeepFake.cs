using System;
using System.Collections;
using System.Collections.Generic;

namespace Phnx.Random.Tests.Fakes
{
    public class DeepFake
    {
        public uint Id { get; set; }

        public ShallowFake Single { get; set; }

        public ShallowFake[] Collection { get; set; }

        public RecursiveFake Recursive { get; set; }

        public CollectionFake Collections { get; set; }
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

    public class CollectionFake
    {
        public IEnumerable Typeless { get; set; }

        public IEnumerable<string> Text { get; set; }

        public IList<int> Numbers { get; set; }

        public ICollection<ushort> Numbers2 { get; set; }

        public List<DateTime> ChangesLog { get; set; }
    }
}
