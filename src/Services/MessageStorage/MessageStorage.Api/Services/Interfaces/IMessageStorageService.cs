using Location.Dto;
using MessageStorage.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageStorage.Api.Services.Interfaces
{
    public interface IMessageStorageService
    {
        Task SaveMessageStorage(SatelliteStorageMessage satelliteStorage);
        Task<List<SatelliteStorageMessage>> GetMessageStorage(List<string> satellites);
    }
}