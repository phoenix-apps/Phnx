using System;

namespace Phnx.Tests.Fakes.TypeFakes
{
    public class TypeWithBrokenConstructor
    {
        public TypeWithBrokenConstructor()
        {
            throw new Exception();
        }
    }
}
