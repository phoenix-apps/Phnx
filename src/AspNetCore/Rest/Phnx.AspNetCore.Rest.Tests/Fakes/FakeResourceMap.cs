using Phnx.AspNetCore.Rest.Services;

namespace Phnx.AspNetCore.Rest.Tests.Fakes
{
    public class FakeResourceMap : IResourceMap<FakeResource, FakeDto, FakePatch>
    {
        public FakeResource MapToData(FakeDto dto)
        {
            if (dto is null) return null;

            return new FakeResource(dto.Id);
        }

        public FakeDto MapToDto(FakeResource data)
        {
            if (data is null) return null;

            return new FakeDto(data);
        }

        public void PatchToData(FakePatch patch, FakeResource data)
        {
        }
    }
}
