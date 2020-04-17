using System;
using Aspose.Cloud.Marketplace.HTML.Converter.Models;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Helpers
{
    /// <summary>
    /// Helper class to convert incoming options into page options for rendering
    /// </summary>
    public static class SettingsConverter
    {
        /// <summary>
        /// Convert incoming options into page settings for rendering.
        /// </summary>
        /// <param name="converterOptions">Options from request</param>
        /// <returns>Page settings for rendering PDF</returns>
        public static PageSettings GetPageSettings(ConverterOptions converterOptions)
        {
            var options = new PageSettings();
            if (converterOptions.Paper != null)
            {
                var paperSize = converterOptions.Paper.Size.ToLower();
                switch (paperSize)
                {
                    case "custom":
                        options.Width = LengthConverter.ToCssPixels(converterOptions.Paper.Width);
                        options.Height = LengthConverter.ToCssPixels(converterOptions.Paper.Height);
                        break;
                    case "a0":
                        options.Width = 3179;
                        options.Height = 4494;
                        break;
                    case "a1":
                        options.Width = 2245;
                        options.Height = 3179;
                        break;
                    case "a2":
                        options.Width = 1587;
                        options.Height = 2245;
                        break;
                    case "a3":
                        options.Width = 1123;
                        options.Height = 1587;
                        break;
                    case "a4":
                        options.Width = 794;
                        options.Height = 1123;
                        break;
                    case "a5":
                        options.Width = 562;
                        options.Height = 794;
                        break;
                    case "a6":
                        options.Width = 397;
                        options.Height = 562;
                        break;
                    case "letter":
                        options.Width = 816;
                        options.Height = 1055;
                        break;
                    case "legal":
                        options.Width = 816;
                        options.Height = 1346;
                        break;
                    default:
                        throw new ArgumentException("Unknown page size.");
                }
                if (!string.IsNullOrWhiteSpace(converterOptions.Paper.Orientation)
                    && converterOptions.Paper.Orientation.ToLower() == "landscape")
                {
                    var temp = options.Width;
                    options.Width = options.Height;
                    options.Height = temp;
                }
            }
            if (converterOptions.Margins != null)
            {
                if (!string.IsNullOrWhiteSpace(converterOptions.Margins.Left))
                    options.Left = LengthConverter.ToCssPixels(converterOptions.Margins.Left);
                if (!string.IsNullOrWhiteSpace(converterOptions.Margins.Right))
                    options.Right = LengthConverter.ToCssPixels(converterOptions.Margins.Right);
                if (!string.IsNullOrWhiteSpace(converterOptions.Margins.Top))
                    options.Top = LengthConverter.ToCssPixels(converterOptions.Margins.Top);
                if (!string.IsNullOrWhiteSpace(converterOptions.Margins.Bottom))
                    options.Bottom = LengthConverter.ToCssPixels(converterOptions.Margins.Bottom);
            }
            return options;
        }
    }
}
