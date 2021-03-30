using Location.Api.Services.Interfaces;
using Location.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Location.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("location")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Gets the location based in 3 points and their distance
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST → /location/
        ///     [
        ///       {
        ///         "distance": 100,
        ///         "x": -500,
        ///         "y": -200
        ///       },
        ///       {
        ///         "distance": 115.5,
        ///         "x": 100,
        ///         "y": -100
        ///       },
        ///       {
        ///         "distance": 142.7,
        ///         "x": 500,
        ///         "y": 100
        ///       }
        ///     ]
        ///
        /// </remarks>
        /// <param name="positionsWithDistance"></param>
        /// <returns>Returns the location</returns>
        /// <response code="200">Returns the location</response>
        /// <response code="404">If the location couldn't be calculated</response>  
        [HttpPost]
        [ProducesResponseType(typeof(Position), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetLocation([FromBody] List<PositionWithDistance> positionsWithDistance)
        {
            var location = _locationService.GetLocation(positionsWithDistance);
            if (location.Ok)
            {
                return Ok(location.Result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
