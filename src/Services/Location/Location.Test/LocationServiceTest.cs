using Location.Api.Services;
using Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Location.Test
{
    public class LocationServiceTest
    {
        private readonly LocationService _locationService = new LocationService();

        [Fact]
        public void Location_not_enough_values()
        {
            var positions = new List<PositionWithDistance>();
            positions.Add(new PositionWithDistance(-137, -132, 173));
            positions.Add(new PositionWithDistance(90, 98, 159));
            var location = _locationService.GetLocation(positions);

            Assert.False(location.Ok);
            Assert.Null(location.Result);
        }

        [Fact]
        public void Location_enough_values()
        {
            var positions = new List<PositionWithDistance>();
            positions.Add(new PositionWithDistance(-500, -200, 100));
            positions.Add(new PositionWithDistance(100, -100, 115.5));
            positions.Add(new PositionWithDistance(500, 100, 142.7));
            var location = _locationService.GetLocation(positions);

            Assert.True(location.Ok);
            Assert.NotNull(location.Result);
            Assert.Equal(-487.2859125, location.Result.X);
            Assert.Equal(1557.014225, location.Result.Y);
        }
    }
}
