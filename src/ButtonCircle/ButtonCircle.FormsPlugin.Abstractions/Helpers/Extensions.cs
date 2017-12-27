using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonCircle.FormsPlugin.Abstractions.Helpers
{
    /// <summary>
    /// Global extension methods for Iconize
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the icon to the list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="key">The key.</param>
        /// <param name="character">The character.</param>
        public static void Add(this IList<IIcon> list, String key, Char character) => list.Add(new Icon(key, character));

        
    }
}
