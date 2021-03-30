using MessageStorage.Api.Services;
using MessageStorage.Dto;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessageStorage.Test
{
    public class MessageStorageServiceTest
    {
        private readonly MessageStorageService _messageStorageService;

        public MessageStorageServiceTest()
        {
            var redisOptions = new RedisCacheOptions();
            redisOptions.Configuration = "localhost";
            redisOptions.InstanceName = "master";
            var redis = new RedisCache(redisOptions);
            _messageStorageService = new MessageStorageService(redis);
        }

        [Fact]
        public async Task Check_Save_And_Get()
        {
            var message = new SatelliteStorageMessage
            {
                Name = "Kenobi",
                Distance = 100,
                Message = new List<string>() { "", "este", "es", "un", "mensaje" }
            };

            await _messageStorageService.SaveMessageStorage(message);
            var messageSaved = await _messageStorageService.GetMessageStorage(new List<string>() { "kenobi" });

            Assert.Single(messageSaved);
            Assert.Equal(message.Name, messageSaved.FirstOrDefault().Name);
            Assert.Equal(message.Distance, messageSaved.FirstOrDefault().Distance);
            Assert.Equal(message.Message, messageSaved.FirstOrDefault().Message);
        }
    }
}
