using MarkSFrancis.Data.EntityFrameworkCore.Seed.Interfaces;
using System.Collections.Generic;

namespace MarkSFrancis.Data.EntityFrameworkCore.Seed
{
    public class SeedGroup : ISeed
    {
        public SeedGroup(params ISeed[] seeds) : this((IEnumerable<ISeed>)seeds)
        {
        }

        public SeedGroup(IEnumerable<ISeed> seeds)
        {
            Seeds = new List<ISeed>(seeds);
        }

        public List<ISeed> Seeds { get; set; }

        public SeedGroup Add(params ISeed[] seeds)
        {
            return Add((IEnumerable<ISeed>)seeds);
        }

        public SeedGroup Add(IEnumerable<ISeed> seeds)
        {
            Seeds.AddRange(seeds);
            return this;
        }

        public void Seed()
        {
            for (int i = 0; i < Seeds.Count; i++)
            {
                Seeds[i].Seed();
            }
        }
    }
}
