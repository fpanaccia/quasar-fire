using MessageStorage.Api.Controllers;
using MessageStorage.Api.Services;
using MessageStorage.Dto;
using Microsoft.AspNetCore.Mvc;
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
    public class MessageStorageControllerTest
    {
        private readonly MessageStorageService _messageStorageService;

        public MessageStorageControllerTest()
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

            var controllerForSave = new MessageStorageController(_messageStorageService);
            await controllerForSave.SaveMessageStorage(message);
            var controllerForGet = new MessageStorageController(_messageStorageService);
            var actionResultForGet = await controllerForGet.GetMessageStorage(new List<string>() { "kenobi" }) as OkObjectResult;

            Assert.Equal((int)System.Net.HttpStatusCode.OK, actionResultForGet.StatusCode);
            Assert.Single((List<SatelliteStorageMessage>)actionResultForGet.Value);
            Assert.Equal(message.Name, ((List<SatelliteStorageMessage>)actionResultForGet.Value).FirstOrDefault().Name);
            Assert.Equal(message.Distance, ((List<SatelliteStorageMessage>)actionResultForGet.Value).FirstOrDefault().Distance);
            Assert.Equal(message.Message, ((List<SatelliteStorageMessage>)actionResultForGet.Value).FirstOrDefault().Message);
        }
    }
}
