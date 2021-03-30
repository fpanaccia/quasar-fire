using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Satellite.Api.Services.Interfaces;
using Satellite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satellite.Api.Services
{
    public class SatelliteService : ISatelliteService
    {
        private readonly IConfiguration _configuration;

        public SatelliteService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<SatellitePosition> GetSatellites()
        {
            var satellitesInService = _configuration["SatellitesInService"];
            return JsonConvert.DeserializeObject<List<SatellitePosition>>(satellitesInService);
        }
    }
}
