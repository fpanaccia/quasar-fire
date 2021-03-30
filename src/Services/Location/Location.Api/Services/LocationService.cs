using Location.Api.Services.Interfaces;
using Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Location.Api.Services
{
    public class LocationService : ILocationService
    {
        /// <summary>
        /// Location math based on wikipedia and this answer https://math.stackexchange.com/a/884851
        /// </summary>
        public ServiceResult<Position> GetLocation(List<PositionWithDistance> positionsWithDistance)
        {
            if (positionsWithDistance == null || (positionsWithDistance != null && (positionsWithDistance.Count != 3 || !positionsWithDistance.Any())))
            {
                return new ServiceResult<Position>(false, null);
            }
            
            try
            {
                var positionsWithDistanceArray = positionsWithDistance.ToArray();
                var firstPoint = positionsWithDistanceArray[0];
                var secondPoint = positionsWithDistanceArray[1];
                var thirdPoint = positionsWithDistanceArray[2];

                var A = 2 * secondPoint.X - 2 * firstPoint.X;
                var B = 2 * secondPoint.Y - 2 * firstPoint.Y;
                var C = Math.Pow(firstPoint.Distance, 2) - Math.Pow(secondPoint.Distance, 2) - Math.Pow(firstPoint.X, 2) + Math.Pow(secondPoint.X, 2) - Math.Pow(firstPoint.Y, 2) + Math.Pow(secondPoint.Y, 2);
                var D = 2 * thirdPoint.X - 2 * secondPoint.X;
                var E = 2 * thirdPoint.Y - 2 * secondPoint.Y;
                var F = Math.Pow(secondPoint.Distance, 2) - Math.Pow(thirdPoint.Distance, 2) - Math.Pow(secondPoint.X, 2) + Math.Pow(thirdPoint.X, 2) - Math.Pow(secondPoint.Y, 2) + Math.Pow(thirdPoint.Y, 2);
                var X = (C * E - F * B) / (E * A - B * D);
                var Y = (C * D - A * F) / (B * D - A * E);
                var position = new Position(X, Y);

                return new ServiceResult<Position>(position);
            }
            catch (Exception)
            {
                return new ServiceResult<Position>(false, null);
            }
        }
    }
}
