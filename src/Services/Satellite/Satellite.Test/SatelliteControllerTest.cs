using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Satellite.Api.Controllers;
using Satellite.Api.Services;
using Satellite.Dto;
using System.Collections.Generic;
using Xunit;

namespace Satellite.Test
{
    public class SatelliteControllerTest
    {
        private readonly SatelliteService _satelliteService;

        public SatelliteControllerTest()
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
            var controller = new SatelliteController(_satelliteService);
            var actionResult = controller.Get() as OkObjectResult;

            Assert.Equal((int)System.Net.HttpStatusCode.OK, actionResult.StatusCode);
            Assert.Equal(3, ((List<SatellitePosition>)actionResult.Value).Count);
        }
    }
}
