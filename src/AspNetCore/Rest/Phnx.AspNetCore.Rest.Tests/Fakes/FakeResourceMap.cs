using Phnx.AspNetCore.Rest.Services;

namespace Phnx.AspNetCore.Rest.Tests.Fakes
{
    public class FakeResourceMap : IResourceMap<FakeResource, FakeDto, FakePatch>
    {
        public FakeResource MapToData(FakeDto dto)
        {
            return new FakeResource(dto.Id);
        }

        public FakeDto MapToDto(FakeResource data)
        {
            return new FakeDto(data);
        }

        public void PatchToData(FakePatch patch, FakeResource data)
        {
            throw new System.NotImplementedException();
        }
    }
}
