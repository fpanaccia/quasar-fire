using Satellite.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Satellite.Api.Services.Interfaces
{
    public interface ISatelliteService
    {
        List<SatellitePosition> GetSatellites();
    }
}