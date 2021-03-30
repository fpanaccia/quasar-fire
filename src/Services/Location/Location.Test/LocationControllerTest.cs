using Location.Api.Controllers;
using Location.Api.Services;
using Location.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Location.Test
{
    public class LocationControllerTest
    {
        private readonly LocationService _locationService = new LocationService();

        [Fact]
        public void Location_not_enough_values()
        {
            var positions = new List<PositionWithDistance>();
            positions.Add(new PositionWithDistance(-500, -200, 100));
            positions.Add(new PositionWithDistance(100, -100, 115.5));

            var controller = new LocationController(_locationService);
            var actionResult = controller.GetLocation(positions) as NotFoundResult;

            Assert.Equal((int)System.Net.HttpStatusCode.NotFound, actionResult.StatusCode);
        }

        [Fact]
        public void Location_enough_values()
        {
            var positions = new List<PositionWithDistance>();
            positions.Add(new PositionWithDistance(-500, -200, 100));
            positions.Add(new PositionWithDistance(100, -100, 115.5));
            positions.Add(new PositionWithDistance(500, 100, 142.7));

            var controller = new LocationController(_locationService);
            var actionResult = controller.GetLocation(positions) as OkObjectResult;

            Assert.Equal((int)System.Net.HttpStatusCode.OK, actionResult.StatusCode);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(-487.2859125, ((Position)actionResult.Value).X);
            Assert.Equal(1557.014225, ((Position)actionResult.Value).Y);
        }
    }
}
