using Message.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Message.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("message")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Gets the complete message based in the message parts
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST → /message/
        ///     [
        ///        [
        ///           "este",
        ///           "",
        ///           "",
        ///           "mensaje",
        ///           ""
        ///        ],
        ///        [
        ///           "",
        ///           "es",
        ///           "",
        ///           "",
        ///           "secreto"
        ///        ],
        ///        [
        ///           "este",
        ///           "",
        ///           "un",
        ///           "",
        ///           ""
        ///        ]
        ///     ]
        ///
        /// </remarks>
        /// <param name="messages"></param>
        /// <returns>Returns the message</returns>
        /// <response code="200">Returns the message</response>
        /// <response code="404">If the message couldn't be calculated</response>  
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMessage([FromBody] List<List<string>> messages)
        {
            var message = _messageService.GetMessage(messages);
            if (message.Ok)
            {
                return Ok(message.Result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
