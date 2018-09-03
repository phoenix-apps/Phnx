using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.TestHelpers
{
    public class ReverseIntComparer : Comparer<int>
    {
        public override int Compare(int x, int y)
        {
            if (x > y)
                return -1;
            else if (x < y)
                return 1;
            else
                return 0;
        }
    }
}
