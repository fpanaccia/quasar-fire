using Radio.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Radio.Api.Services.Interfaces
{
    public interface IRadioService
    {
        Task<ServiceResult<DecodedMessage>> ProcessTopSecretMessage(TopSecretMessage message);
        Task<ServiceResult<bool>> SaveMessageStorage(SatelliteMessage satelliteMessage);
        Task<ServiceResult<DecodedMessage>> GetMessageFromStorage();
    }
}