namespace Aspose.Cloud.Marketplace.HTML.Converter.Models
{
    /// <summary>
    /// Settings for printable pages
    /// </summary>
    public class Paper
    {
        /// <summary>
        /// Gets or sets the page orientation.
        /// </summary>
        public string Orientation { get; set; }
        /// <summary>
        /// Gets or sets the page size. 
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Gets or sets the Width. Used when Size is Custom
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// Gets or sets the Height. Used when Size is Custom
        /// </summary>
        public string Height { get; set; }
    }
}