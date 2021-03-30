using Microsoft.AspNetCore.Mvc;
using Satellite.Api.Services.Interfaces;
using Satellite.Dto;
using System.Collections.Generic;
using System.Net;

namespace Satellite.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("satellite")]
    public class SatelliteController : ControllerBase
    {
        private readonly ISatelliteService _satelliteService;
        public SatelliteController(ISatelliteService satelliteService)
        {
            _satelliteService = satelliteService;
        }

        /// <summary>
        /// Gets the satellites and their positions
        /// </summary>
        /// <returns>Array of satellites and their positions</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<SatellitePosition>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var satellites = _satelliteService.GetSatellites();
            return Ok(satellites);
        }
    }
}
