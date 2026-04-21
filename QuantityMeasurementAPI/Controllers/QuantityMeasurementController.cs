using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuantityMeasurementModel;
using QuantityMeasurementService;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/v1/quantities")] 
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IMemoryCache _cache;
        public QuantityMeasurementController(IQuantityMeasurementService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityInputDTO request)
        {
            return Ok(_service.Add(request));
        }
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] QuantityInputDTO request)
        {
            return Ok(_service.Compare(request));
        }
        [HttpPost("convert")]
        public IActionResult Convert([FromBody] QuantityInputDTO request)
        {
            return Ok(_service.Convert(request));
        }
        [HttpGet("history/type/{type}")]
        public IActionResult GetHistoryByType(string type)
        {
            return Ok(_service.GetHistoryByType(type));
        }
        [HttpGet("history/operation/{operation}")]
        public IActionResult GetHistoryByOperation(string operation)
        {
            return Ok(_service.GetHistoryByOperation(operation));
        }
        [HttpGet("history/errored")]
        public IActionResult GetErroredHistory()
        {
            return Ok(_service.GetErroredHistory());
        }
        [HttpGet("count/{operation}")]
        public IActionResult GetOperationCount(string operation)
        {
            return Ok(_service.GetOperationCount(operation));
        }
        [HttpPost("draft/save/{userId}")]
        public IActionResult SaveDraft(string userId, [FromBody] QuantityInputDTO incompleteData)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            _cache.Set($"Draft_{userId}", incompleteData, cacheOptions);
            return Ok(new { message = $"Draft securely saved in server cache for user {userId}!" });
        }

        [HttpGet("draft/restore/{userId}")]
        public IActionResult RestoreDraft(string userId)
        {
            if (_cache.TryGetValue($"Draft_{userId}", out QuantityInputDTO savedDraft))
            {
                return Ok(savedDraft);
            }
            return NotFound(new { message = "No draft found. It may have expired or was never saved." });
        }
    }
}