using AspNetCoreSoapAuthBasicService.SoapServices;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSoapAuthBasicService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IConfiguration _configuration;

        public ServiceController(
            ILogger<ServiceController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var hostName = _configuration.GetSection("Service:HostName").Value;

            hostName += "/BasicAuthDemoSoapService.asmx";

            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>AspNetCoreSoapAuthBasicService</title>
                </head>
                <body>
                    <ul>
                        <li><a href={hostName}>{hostName}</a></li>
                    </ul>
                </body>
                </html>";

            return new ContentResult() { Content = htmlContent, ContentType= "text/html" };
        }

    }
}
