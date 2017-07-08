using System;
using System.Linq;
using Xamarin.Forms;

namespace ButtonCircle.FormsPlugin.Abstractions
{
    public class IconsConverter : TypeConverter
    {
        /// <summary>
        /// Determines whether this instance [can convert from] the specified source type.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public override Boolean CanConvertFrom(Type sourceType)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");
            return sourceType == typeof(String);
        }

        /// <summary>
        /// Convert from invariant String.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override object ConvertFromInvariantString(string value)
        {
            if (value == null)
                return null;

            var valuestring = value as String;
            if (valuestring != null)
            {
                var fieldInfo = System.Reflection.RuntimeReflectionExtensions.GetRuntimeFields(typeof(Icons)).FirstOrDefault(x => x.Name == valuestring);
                if (fieldInfo != null)
                {
                    return (Icons)fieldInfo.GetValue(null);
                }
            }


            throw new InvalidOperationException(String.Format("Cannot convert {0} into {1}", value, typeof(Icons)));
        }
    }
}
