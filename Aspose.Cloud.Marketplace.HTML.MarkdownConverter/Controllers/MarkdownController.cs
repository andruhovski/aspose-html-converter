using System;
using System.Text;
using Aspose.Cloud.Marketplace.HTML.Converter.Helpers;
using Aspose.Cloud.Marketplace.HTML.Converter.Models;
using Aspose.Cloud.Marketplace.HTML.Converter.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Aspose.Cloud.Marketplace.HTML.Converter.Controllers
{
    /// <summary>
    /// API controller for Markdown handling
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MarkdownController : ControllerBase
    {
        private string _contentType;
        private readonly ILogger<MarkdownController> _logger;
        private readonly Aspose.Html.Cloud.Sdk.Api.Interfaces.IImportApi _importApi;
        private readonly Aspose.Html.Cloud.Sdk.Api.Interfaces.IConversionApi _conversionApi;
        private readonly Aspose.Html.Cloud.Sdk.Api.Interfaces.IStorageFileApi _storageApi;
        private readonly IWebHostEnvironment _env;
        private readonly StatisticalService _statisticalService;

        /// <summary>
        /// Constructor. Initialize services for conversion
        /// </summary>
        /// <param name="logger">Logger service</param>
        /// <param name="env">Host environment</param>
        /// <param name="conversionApi">HTML-to-PDF API</param>
        /// <param name="importApi">MD-to-PDF API</param>
        /// <param name="storageApi">Storage API</param>
        /// <param name="statisticalService">Statistical Service</param>
        public MarkdownController(
            ILogger<MarkdownController> logger,
            IWebHostEnvironment env,
            Aspose.Html.Cloud.Sdk.Api.Interfaces.IConversionApi conversionApi,
            Aspose.Html.Cloud.Sdk.Api.Interfaces.IImportApi importApi,
            Aspose.Html.Cloud.Sdk.Api.Interfaces.IStorageFileApi storageApi,
            StatisticalService statisticalService)
        {
            _logger = logger;
            _env = env;
            _importApi = importApi;
            _storageApi = storageApi;
            _conversionApi = conversionApi;
            _statisticalService = statisticalService;
        }

        /// <summary>
        /// Converter. Accepts incoming options, calculate settings for printable pages (if necessary) and render output content.
        /// </summary>
        /// <param name="converterOptions">Incoming options</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ConverterOptions converterOptions)
        {
            System.IO.Stream resultStream = null;
            var startTime = DateTime.Now;

            var tempFileGuid = Guid.NewGuid();
            _logger.LogInformation($"Conversion started: {tempFileGuid} from MD to {converterOptions.To.ToUpper()} / {startTime}");
            var htmlFileName = tempFileGuid + ".html";
            var cssFileTheme = System.IO.Path.Combine(_env.WebRootPath, "css/markdown.css");
            var cssContent = System.IO.File.ReadAllText(cssFileTheme);
            var resultedContent = converterOptions.To.ToLower() != "html"
                ? string.Concat("<style>", cssContent, "</style>\n\n", converterOptions.Content)
                : converterOptions.Content;

            var file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(resultedContent));
            Aspose.Html.Cloud.Sdk.Api.Model.AsposeResponse response;
            try
            {
                response = _importApi.PostImportMarkdownToHtml(file, htmlFileName);
            }
            catch (AggregateException ae)
            {
                ae.Handle((x) =>
                {
                    _logger.LogError($"Error file uploading {tempFileGuid}. {x.Message}");
                    return true;
                });
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error file uploading {tempFileGuid}. {ex.Message}");
                throw;
            }


            switch (converterOptions.To.ToLower())
            {
                case "pdf":
                    _logger.LogInformation($"Converter options: {converterOptions.Paper.Size} {converterOptions.Paper.Width}x{converterOptions.Paper.Height}");
                    var options = SettingsConverter.GetPageSettings(converterOptions);
                    _logger.LogInformation($"Calculated options: {options}");
                    var streamResponse = _conversionApi.GetConvertDocumentToPdf(htmlFileName,
                        options.Width, options.Height,
                        options.Left, options.Bottom,
                        options.Right, options.Top);
                    if (streamResponse != null && streamResponse.Status == "OK")
                    {
                        try
                        {
                            resultStream = streamResponse.ContentStream;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Conversion error: {ex.Message}");
                            throw;
                        }
                        _contentType = "application/pdf";
                    }
                    break;
                case "html":
                    try
                    {
                        if (response != null && response.Status == "OK")
                            resultStream = _storageApi.DownloadFile(htmlFileName).ContentStream;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error file downloading {0} {1}", tempFileGuid, ex.Message);
                        throw;
                    }

                    _contentType = "text/html; charset=utf-8";
                    break;
                default:
                    throw new NotImplementedException($"MD-to-{converterOptions.To} is not supported.");
            }
            if (resultStream == null)
            {
                _logger.LogError("Error MD to PDF conversion {0} {1}", tempFileGuid, htmlFileName);
                throw new Exception("Error MD to PDF conversion");
            }
            var finishTime = DateTime.Now;
            _logger.LogInformation(message: $"Conversion completed: {tempFileGuid} / {finishTime}");
            _logger.LogInformation(message: $"Conversion time: {tempFileGuid} / {finishTime.Subtract(startTime).Milliseconds}ms");
            //TODO: Implement gather stats
            _statisticalService.IncrementCounter(converterOptions.MachineId);
            return File(resultStream, _contentType);
        }
    }
}