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
    public class LocationService
    {
        public HttpClient _client { get; }
        private readonly IConfiguration _configuration;

        public LocationService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            client.BaseAddress = new Uri($"http://{_configuration["location"]}/");
            _client = client;
        }

        public async Task<ServiceResult<Position>> GetLocation(List<PositionWithDistance> positionsWithDistance)
        {
            var messagesJson = new StringContent(
                JsonConvert.SerializeObject(positionsWithDistance),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/location", messagesJson);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentString = await httpResponse.Content.ReadAsStringAsync();
                var parsedContent = JsonConvert.DeserializeObject<Position>(contentString);
                return new ServiceResult<Position>(parsedContent);
            }
            else
            {
                return new ServiceResult<Position>(false, null);
            }
        }
    }
}
