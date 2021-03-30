using MessageStorage.Api.Services.Interfaces;
using MessageStorage.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MessageStorage.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("messageStorage")]
    public class MessageStorageController : ControllerBase
    {
        private readonly IMessageStorageService _messageStorageService;
        public MessageStorageController(IMessageStorageService messageStorageService)
        {
            _messageStorageService = messageStorageService;
        }

        /// <summary>
        /// Store message and distance for a satellite
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST → /messageStorage/
        ///     {
        ///        "name":"Kenobi",
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
        /// <param name="satelliteStorage"></param>
        /// <returns></returns>
        /// <response code="200">When the data was saved</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task SaveMessageStorage([FromBody] SatelliteStorageMessage satelliteStorage)
        {
            await _messageStorageService.SaveMessageStorage(satelliteStorage);
        }

        /// <summary>
        /// Get the stored messages and distances for the satellites
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST → /messageStorage/GetStored
        ///     [
        ///         "kenobi"
        ///     ]
        ///
        /// </remarks>
        /// <param name="satellites"></param>
        /// <returns></returns>
        /// <response code="200">Returns the message and distance for the satellites</response>
        [HttpPost("GetStored")]
        [ProducesResponseType(typeof(List<SatelliteStorageMessage>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessageStorage([FromBody] List<string> satellites)
        {
            return Ok(await _messageStorageService.GetMessageStorage(satellites));
        }
    }
}
