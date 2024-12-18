using IPLookup.Models;
using IPLookup.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPLookup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IpLookupController : ControllerBase
    {
        private readonly IIpLookupService _ipLookupService;

        public IpLookupController(IIpLookupService ipLookupService)
        {
            _ipLookupService = ipLookupService;
        }

        /// <summary>
        /// Retrieves details about the specified IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address to retrieve details for.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the IP address details or an appropriate error response.
        /// </returns>
        [HttpGet("{ipAddress}")]
        [ProducesResponseType(typeof(IpDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IpDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IpDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IpDetails), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetIpDetails(string ipAddress)
        {
            var result = await _ipLookupService.GetIpDetailsAsync(ipAddress);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
