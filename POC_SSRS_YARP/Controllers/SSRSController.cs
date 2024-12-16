using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace POC_SSRS_YARP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SSRSController : ControllerBase
    {
        private readonly ILogger<SSRSController> _logger;
        private readonly IConfiguration _configuration;
        public SSRSController(ILogger<SSRSController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("ReportLinkEmbed/{reportName}")]
        public IActionResult GetReportLinkEmbed(string reportName)
        {
            string URL_REPORT = $"{Request.Scheme}://{Request.Host}/";
            URL_REPORT += "reports/report";
            URL_REPORT += _configuration.GetValue<string>("ReportServerPath");
            URL_REPORT += $"{reportName}?rs:embed=true";

            var queryString = Request.QueryString.ToString();

            if (!string.IsNullOrEmpty(queryString))
                URL_REPORT += queryString.Replace('?', '&');

            return Ok(URL_REPORT);
        }
    }
}
