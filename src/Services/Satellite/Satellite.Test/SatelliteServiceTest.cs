using Microsoft.Extensions.Configuration;
using Moq;
using Satellite.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Satellite.Test
{
    public class SatelliteServiceTest
    {
        private readonly SatelliteService _satelliteService;

        public SatelliteServiceTest()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            _satelliteService = new SatelliteService(configuration);
        }

        [Fact]
        public void Satellites_All()
        {
            var satellites = _satelliteService.GetSatellites();

            Assert.Equal(3, satellites.Count);
        }
    }
}
