using System;

namespace MarkSFrancis.Tests.Fakes.TypeFakes
{
    public class TypeWithBrokenConstructor
    {
        public TypeWithBrokenConstructor()
        {
            throw new Exception();
        }
    }
}
