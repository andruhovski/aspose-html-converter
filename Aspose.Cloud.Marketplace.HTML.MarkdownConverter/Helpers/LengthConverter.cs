using System;
using System.Globalization;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Helpers
{
    /// <summary>
    /// Helper class for length conversion
    /// </summary>
    public static class LengthConverter
    {
        private static readonly CultureInfo Provider = new System.Globalization.CultureInfo("us");
        /// <summary>
        /// Convert measurement unit to points
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Value in pt (72pt == 1 inch)</returns>
        public static int ToPoints(string parameter)
        {
            var pos = parameter.LastIndexOfAny("0123456789".ToCharArray());
            var value = double.Parse(parameter.Substring(0, pos + 1), Provider);
            var unit = parameter.Substring(pos + 1).Trim().ToLower();
            value *= unit switch
            {
                "in" => 72,
                "mm" => 2.83465,
                "cm" => 28.3465,
                "px" => 0.75,
                _ => throw new ArgumentException("Unknown measurement units"),
            };
            return Convert.ToInt32(value);
        }
        /// <summary>
        /// Convert measurement unit to CSS pixels
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>CSS pixels value (96dpi)</returns>
        public static int ToCssPixels(string parameter)
        {
            var pos = parameter.LastIndexOfAny("0123456789".ToCharArray());
            var value = double.Parse(parameter.Substring(0, pos + 1), Provider);
            var unit = parameter.Substring(pos + 1).Trim().ToLower();
            value *= unit switch
            {
                "in" => 96,
                "mm" => 3.779527559,
                "cm" => 37.79527559,
                "px" => 1,
                _ => throw new ArgumentException("Unknown measurement units"),
            };
            return Convert.ToInt32(value);
        }
    }
}