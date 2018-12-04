namespace Phnx.AspNetCore.ETags.Tests.Fakes
{
    public class FakeDto
    {
        public FakeDto()
        {
        }

        public FakeDto(FakeResource resource)
        {
            Id = resource.Id;
        }

        public int Id { get; set; }
    }
}
