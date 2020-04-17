namespace Aspose.Cloud.Marketplace.HTML.Converter.Models
{
    /// <summary>
    /// PageSettings are used to render printed pages
    /// </summary>
    public class PageSettings
    {
        /// <summary>
        /// Height. Default value is 297mm
        /// </summary>
        public int Height = 1123;
        /// <summary>
        /// Width. Default value is 210mm
        /// </summary>
        public int Width = 794;
        /// <summary>
        /// Left margin. Default value is210mm
        /// </summary>
        public int Left = 38;
        /// <summary>
        /// Bottom margin. Default value is210mm
        /// </summary>
        public int Bottom = 38;
        /// <summary>
        /// Right margin. Default value is210mm
        /// </summary>
        public int Right = 38;
        /// <summary>
        /// Top margin. Default value is210mm
        /// </summary>
        public int Top = 38;
        /// <summary>
        /// string representation (for logs)
        /// </summary>
        /// <returns>string: HxW [T/B L/R]</returns>
        public override string ToString() => $"{Height}x{Width} [{Top}/{Bottom} {Left}/{Right}]";
    }
}