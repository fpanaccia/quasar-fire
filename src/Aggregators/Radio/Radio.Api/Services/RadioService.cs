using Radio.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio.Api.Services.Interfaces
{
    public class RadioService : IRadioService
    {
        private readonly LocationService _locationService;
        private readonly MessageService _messageService;
        private readonly MessageStorageService _messageStorageService;
        private readonly SatelliteService _satelliteService;

        public RadioService(LocationService locationService, MessageService messageService, MessageStorageService messageStorageService, SatelliteService satelliteService)
        {
            _locationService = locationService;
            _messageService = messageService;
            _messageStorageService = messageStorageService;
            _satelliteService = satelliteService;
        }

        public async Task<ServiceResult<DecodedMessage>> ProcessTopSecretMessage(TopSecretMessage message)
        {
            var satelliteResult = await _satelliteService.GetSatellites();
            if (!satelliteResult.Ok)
            {
                return new ServiceResult<DecodedMessage>(false, null);
            }

            var satelliteMessages = message.Satellites;
            var satelliteNames = satelliteMessages.Select(x => x.Name.ToLower()).Distinct();
            if (satelliteNames.Count() != satelliteResult.Result.Count() || satelliteNames.Any(x => !satelliteResult.Result.Any(y => y.Name.ToLower() == x.ToLower())))
            {
                return new ServiceResult<DecodedMessage>(false, null);
            }

            return await GetMessage(satelliteMessages, satelliteResult.Result);
        }

        public async Task<ServiceResult<bool>> SaveMessageStorage(SatelliteMessage satelliteMessage)
        {
            return await _messageStorageService.SaveMessageStorage(satelliteMessage);
        }

        public async Task<ServiceResult<DecodedMessage>> GetMessageFromStorage()
        {
            var satelliteResult = await _satelliteService.GetSatellites();
            if (!satelliteResult.Ok)
            {
                return new ServiceResult<DecodedMessage>(false, null);
            }

            var satelliteNames = satelliteResult.Result.Select(x => x.Name.ToLower()).Distinct().ToList();
            var savedMessages = await _messageStorageService.GetMessageStorage(satelliteNames);
            if (!savedMessages.Ok || savedMessages.Result.Count() != satelliteNames.Count())
            {
                return new ServiceResult<DecodedMessage>(false, null);
            }

            return await GetMessage(savedMessages.Result.ToArray(), satelliteResult.Result);
        }

        private async Task<ServiceResult<DecodedMessage>> GetMessage(SatelliteMessage[] satelliteMessages, List<SatellitePosition> satelliteResults)
        {
            var locations = BuildPositions(satelliteMessages, satelliteResults);
            var messages = BuildMessages(satelliteMessages, satelliteResults);
            var calculatedLocationTask = _locationService.GetLocation(locations);
            var parsedMessageTask = _messageService.GetMessage(messages);
            //Parallel
            await Task.WhenAll(calculatedLocationTask, parsedMessageTask);
            var calculatedLocation = await calculatedLocationTask;
            var parsedMessage = await parsedMessageTask;
            if (calculatedLocation.Ok && parsedMessage.Ok)
            {
                var decodedMessage = new DecodedMessage(calculatedLocation.Result.X, calculatedLocation.Result.Y, parsedMessage.Result);
                return new ServiceResult<DecodedMessage>(decodedMessage);
            }
            else
            {
                return new ServiceResult<DecodedMessage>(false, null);
            }
        }

        private List<PositionWithDistance> BuildPositions(SatelliteMessage[] satelliteMessages, List<SatellitePosition> satelliteResults)
        {
            var locations = new List<PositionWithDistance>();
            foreach (var satellite in satelliteResults)
            {
                if (satelliteMessages.Any(x => x.Name.ToLower() == satellite.Name.ToLower()))
                {
                    var location = new PositionWithDistance();
                    location.X = satellite.X;
                    location.Y = satellite.Y;
                    location.Distance = satelliteMessages.LastOrDefault(x => x.Name.ToLower() == satellite.Name.ToLower()).Distance;
                    locations.Add(location);
                }
            }

            return locations;
        }

        private List<List<string>> BuildMessages(SatelliteMessage[] satelliteMessages, List<SatellitePosition> satelliteResults)
        {
            var messages = new List<List<string>>();
            foreach (var satellite in satelliteResults)
            {
                if (satelliteMessages.Any(x => x.Name.ToLower() == satellite.Name.ToLower()))
                {
                    var message = satelliteMessages.LastOrDefault(x => x.Name.ToLower() == satellite.Name.ToLower()).Message.ToList();
                    messages.Add(message);
                }
            }

            return messages;
        }

    }
}