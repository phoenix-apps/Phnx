using System.Collections.Generic;
using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random color name. Source: <see cref="ColorNames"/>
    /// </summary>
    public class RandomColorName : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random color name
        /// </summary>
        /// <returns>A random color name</returns>
        public string Get()
        {
            return RandomHelper.OneOf2D(new List<IList<string>>
            {
                ColorNames.SimpleColors,
                ColorNames.BrowserColors,
                ColorNames.ComplexColors
            });
        }

        /// <summary>
        /// Get a random simple color name (such as red, blue or orange)
        /// </summary>
        /// <returns>A random simple color</returns>
        public string GetSimpleColor()
        {
            return RandomHelper.OneOf(ColorNames.SimpleColors);
        }

        /// <summary>
        /// Get a random web-browser color (such as Alice Blue, Linen or Sienna)
        /// </summary>
        /// <returns>A random web-browser color</returns>
        public string GetBrowserColor()
        {
            return RandomHelper.OneOf(ColorNames.BrowserColors);
        }

        /// <summary>
        /// Get a random complex color (such as Prussian blue, Viridian or Ivory)
        /// </summary>
        /// <returns>A random complex color</returns>
        public string GetComplexColor()
        {
            return RandomHelper.OneOf(ColorNames.ComplexColors);
        }
    }
}
