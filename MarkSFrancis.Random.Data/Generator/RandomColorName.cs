using System.Collections.Generic;
using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    public class RandomColorName : IRandomGenerator<string>
    {
        public string Get()
        {
            return RandomHelper.OneOf2D(new List<IList<string>>
            {
                ColorNames.SimpleColors,
                ColorNames.BrowserColors,
                ColorNames.ComplexColors
            });
        }

        public string GetSimpleColor()
        {
            return RandomHelper.OneOf(ColorNames.SimpleColors);
        }

        public string GetBrowserColor()
        {
            return RandomHelper.OneOf(ColorNames.BrowserColors);
        }

        public string GetComplexColor()
        {
            return RandomHelper.OneOf(ColorNames.ComplexColors);
        }
    }
}
