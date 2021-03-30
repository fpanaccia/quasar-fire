using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Radio.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Radio.Api.Services
{
    public class SatelliteService
    {
        public HttpClient _client { get; }
        private readonly IConfiguration _configuration;

        public SatelliteService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            client.BaseAddress = new Uri($"http://{_configuration["satellite"]}/");
            _client = client;
        }

        public async Task<ServiceResult<List<SatellitePosition>>> GetSatellites()
        {
            var httpResponse = await _client.GetAsync($"/satellite");
            if(httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentString = await httpResponse.Content.ReadAsStringAsync();
                var parsedContent = JsonConvert.DeserializeObject<List<SatellitePosition>>(contentString);
                return new ServiceResult<List<SatellitePosition>>(parsedContent);
            }
            else
            {
                return new ServiceResult<List<SatellitePosition>>(false, null);
            }
        }
    }
}
