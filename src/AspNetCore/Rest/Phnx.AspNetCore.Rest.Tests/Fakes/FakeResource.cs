using Phnx.AspNetCore.Rest.Models;
using System;

namespace Phnx.AspNetCore.Rest.Tests.Fakes
{
    public class FakeResource : IResourceDataModel
    {
        private static Random rnd = new Random();

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

        public string ConcurrencyStamp { get; }

        public int Id { get; set; }
    }
}
