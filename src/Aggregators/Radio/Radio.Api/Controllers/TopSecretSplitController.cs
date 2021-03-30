using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Radio.Api.Services.Interfaces;
using Radio.Dto;
using System.Net;
using System.Threading.Tasks;

namespace Radio.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("topsecret_split")]
    public class TopSecretSplitController : ControllerBase
    {
        private readonly IRadioService _radioService;

        public TopSecretSplitController(IRadioService radioService)
        {
            _radioService = radioService;
        }

        /// <summary>
        /// Obtains location and message utilizing the information previously stored
        /// </summary>
        /// <returns>Location and Message</returns>
        /// <response code="200">Returns the location and message</response>
        /// <response code="404">If the location or message couldn't be calculated</response> 
        [HttpGet]
        [ProducesResponseType(typeof(DecodedMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var savedMessages = await _radioService.GetMessageFromStorage();
            if (savedMessages.Ok)
            {
                return Ok(savedMessages.Result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Store message and distance for a satellite
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST → /topsecret_split/{satelliteName}
        ///     {
        ///        "distance":100.0,
        ///        "message":[
        ///           "",
        ///           "este",
        ///           "es",
        ///           "un",
        ///           "mensaje"
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="satelliteName"></param>
        /// <param name="distanceWithMessage"></param>
        /// <returns></returns>
        /// <response code="200">When the data was saved</response>
        /// <response code="404">When we couldnt saved the data</response> 
        [HttpPost("{satelliteName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post(string satelliteName, [FromBody] DistanceWithMessage distanceWithMessage)
        {
            var satelliteMessage = new SatelliteMessage(satelliteName, distanceWithMessage);
            var saveMessage = await _radioService.SaveMessageStorage(satelliteMessage);
            if (saveMessage.Ok)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
