using System;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Models
{
    /// <summary>
    /// Class holds incoming options.
    /// </summary>
    public class ConverterOptions
    {
        /// <summary>
        /// Machine ID. Used for stats purposes.
        /// </summary>
        public string MachineId { get; set; }
        /// <summary>
        /// Markdown content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Desired format for output file
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Page settings 
        /// </summary>
        public Paper Paper { get; set; }
        /// <summary>
        /// Margin settings 
        /// </summary>
        public Margins Margins { get; set; }
    }
}