using AutoMapper;
using BatchProcessing.Models;
using BatchProcessing.Services;
using Microsoft.AspNetCore.Mvc;

namespace BatchProcessing.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IpBatchController : ControllerBase
    {
        private readonly IBatchService _batchService;
        private readonly IMapper _mapper;

        public IpBatchController(IBatchService batchService, IMapper mapper)
        {
            _batchService = batchService;
            _mapper = mapper;
        }

        /// <summary>
        /// Starts a new batch process for a list of IP addresses.
        /// </summary>
        /// <param name="request">The batch request containing the IP addresses.</param>
        /// <returns>Returns the batch ID assigned to the batch.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBatch([FromBody] BatchRequest request)
        {
            var batchId = _batchService.StartBatchProcessing(request);

            return CreatedAtAction(nameof(CreateBatch), batchId);
        }

        /// <summary>
        /// Retrieves the status of a previously started batch.
        /// </summary>
        /// <param name="batchId">The ID of the batch to query.</param>
        /// <returns>The current status of the batch, including processed count and errors if any.</returns>
        [HttpGet("{batchId}")]
        [ProducesResponseType(typeof(BatchStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BatchStatusResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BatchStatusResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetBatchStatus(Guid batchId)
        {
            var record = _batchService.GetBatch(batchId);
            if (record is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BatchStatusResponse>(record));
        }
    }
}
