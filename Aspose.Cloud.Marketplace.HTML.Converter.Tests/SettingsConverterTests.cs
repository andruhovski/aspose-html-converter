using Aspose.Cloud.Marketplace.HTML.Converter.Helpers;
using Aspose.Cloud.Marketplace.HTML.Converter.Models;
using Xunit;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Tests
{
    public class SettingsConverterTests
    {
        [Fact]
        public void GetPageSettings_MarginsSetNull_ExpectDefault()
        {
            var convertOptions = new ConverterOptions();
            var actual = SettingsConverter.GetPageSettings(convertOptions);
            Assert.Equal(38, actual.Left);
            Assert.Equal(38, actual.Top);
            Assert.Equal(38, actual.Right);
            Assert.Equal(38, actual.Bottom);
        }

        [Fact]
        public void GetPageSettings_PaperSizeSetNull_ExpectDefault()
        {
            var convertOptions = new ConverterOptions();
            var actual = SettingsConverter.GetPageSettings(convertOptions);
            Assert.Equal(794, actual.Width);
            Assert.Equal(1123, actual.Height);
        }

        [Fact]
        public void GetPageSettings_PaperSizeSetA4Landscape_ExpectA4Landscape()
        {
            var convertOptions = new ConverterOptions {Paper = new Paper {Size = "A4", Orientation = "Landscape"}};
            var actual = SettingsConverter.GetPageSettings(convertOptions);
            Assert.Equal(794, actual.Height);
            Assert.Equal(1123, actual.Width);
        }

        [Fact]
        public void GetPageSettings_PaperSizeSetA5Portrait_ExpectA5Portrait()
        {
            var convertOptions = new ConverterOptions { Paper = new Paper { Size = "A5"} };
            var actual = SettingsConverter.GetPageSettings(convertOptions);
            Assert.Equal(794, actual.Height);
            Assert.Equal(562, actual.Width);
        }
    }
}
