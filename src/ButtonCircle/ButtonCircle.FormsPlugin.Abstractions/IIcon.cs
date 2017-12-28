using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonCircle.FormsPlugin.Abstractions
{
    /// <summary>
    /// Icon represents one icon in an icon font.
    /// </summary>
    public interface IIcon
    {
        /// <summary>
        /// The key of icon, for example 'fa-ok'
        /// </summary>
        /// <returns></returns>
        String Key { get; }

        /// <summary>
        /// The character matching the key in the font, for example '\u4354'
        /// </summary>
        /// <returns></returns>
        Char Character { get; }
        
    }
}
