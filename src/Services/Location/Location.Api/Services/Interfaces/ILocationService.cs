using Location.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Location.Api.Services.Interfaces
{
    public interface ILocationService
    {
        ServiceResult<Position> GetLocation(List<PositionWithDistance> positionWithDistance);
    }
}