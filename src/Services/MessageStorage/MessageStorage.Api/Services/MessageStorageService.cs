using Location.Dto;
using MessageStorage.Api.Services.Interfaces;
using MessageStorage.Dto;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageStorage.Api.Services
{
    public class MessageStorageService : IMessageStorageService
    {
        private readonly IDistributedCache _redis;

        public MessageStorageService(IDistributedCache redis)
        {
            _redis = redis;
        }

        public async Task<List<SatelliteStorageMessage>> GetMessageStorage(List<string> satellites)
        {
            var ret = new List<SatelliteStorageMessage>();

            foreach (var satellite in satellites)
            {
                var messageString = await _redis.GetStringAsync(satellite.ToLower());
                if(!string.IsNullOrWhiteSpace(messageString))
                {
                    var parsedMessage = JsonConvert.DeserializeObject<SatelliteStorageMessage>(messageString);
                    ret.Add(parsedMessage);
                }
            }

            return ret;
        }

        public async Task SaveMessageStorage(SatelliteStorageMessage satelliteStorage)
        {
            var storageData = JsonConvert.SerializeObject(satelliteStorage);
            await _redis.SetStringAsync(satelliteStorage.Name.ToLower(), storageData);
        }
    }
}
