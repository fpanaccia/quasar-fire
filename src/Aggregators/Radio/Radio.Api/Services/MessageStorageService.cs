using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Radio.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Radio.Api.Services
{
    public class MessageStorageService
    {
        public HttpClient _client { get; }
        private readonly IConfiguration _configuration;

        public MessageStorageService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            client.BaseAddress = new Uri($"http://{_configuration["messageStorage"]}/");
            _client = client;
        }

        public async Task<ServiceResult<List<SatelliteMessage>>> GetMessageStorage(List<string> satellites)
        {
            var satellitesJson = new StringContent(
                JsonConvert.SerializeObject(satellites),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/messageStorage/GetStored", satellitesJson);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<List<SatelliteMessage>>(contentString);
                return new ServiceResult<List<SatelliteMessage>>(content);
            }
            else
            {
                return new ServiceResult<List<SatelliteMessage>>(false, null);
            }
        }

        public async Task<ServiceResult<bool>> SaveMessageStorage(SatelliteMessage satelliteMessage)
        {
            var satelliteMessageJson = new StringContent(
                JsonConvert.SerializeObject(satelliteMessage),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/messageStorage", satelliteMessageJson);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ServiceResult<bool>(true);
            }
            else
            {
                return new ServiceResult<bool>(false, false);
            }
        }
    }
}
