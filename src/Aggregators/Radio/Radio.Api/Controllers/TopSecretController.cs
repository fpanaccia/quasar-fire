using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Radio.Api.Services;
using Radio.Api.Services.Interfaces;
using Radio.Dto;
using System.Net;
using System.Threading.Tasks;

namespace Radio.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("topsecret")]
    public class TopSecretController : ControllerBase
    {
        private readonly IRadioService _radioService;

        public TopSecretController(IRadioService radioService)
        {
            _radioService = radioService;
        }

        /// <summary>
        /// Obtains location and message utilizing the information provided by the satellites
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST → /topsecret/
        ///     {
        ///         "satellites": [
        ///             {
        ///                 "name": "kenobi",
        ///                 "distance": 100.0,
        ///                 "message": ["este", "", "", "mensaje", ""]
        ///             },
        ///             {
        ///                 "name": "skywalker",
        ///                 "distance": 115.5,
        ///                 "message": ["", "es", "", "", "secreto"]
        ///             },
        ///             {
        ///                 "name": "sato",
        ///                 "distance": 142.7,
        ///                 "message": ["este", "", "un", "", ""]
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="message"></param>
        /// <returns>Location and Message</returns>
        /// <response code="200">Returns the location and message</response>
        /// <response code="404">If the location or message couldn't be calculated</response>  
        [HttpPost]
        [ProducesResponseType(typeof(DecodedMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post([FromBody] TopSecretMessage message)
        {
            var topSecretMessage = await _radioService.ProcessTopSecretMessage(message);
            if (topSecretMessage.Ok)
            {
                return Ok(topSecretMessage.Result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
