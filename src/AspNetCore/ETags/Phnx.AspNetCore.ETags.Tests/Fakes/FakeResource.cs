using System;
using System.ComponentModel.DataAnnotations;

namespace Phnx.AspNetCore.ETags.Tests.Fakes
{
    public class FakeResource
    {
        private static readonly Random rnd = new Random();

        public FakeResource(int id)
        {
            Id = Id;
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }

        public FakeResource(string concurrencyStamp)
        {
            ConcurrencyStamp = concurrencyStamp;
            Id = rnd.Next();
        }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; }

        public int Id { get; set; }
    }
}
