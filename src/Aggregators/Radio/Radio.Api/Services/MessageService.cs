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
    public class MessageService
    {
        public HttpClient _client { get; }
        private readonly IConfiguration _configuration;

        public MessageService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            client.BaseAddress = new Uri($"http://{_configuration["message"]}/");
            _client = client;
        }

        public async Task<ServiceResult<string>> GetMessage(List<List<string>> messages)
        {
            var messagesJson = new StringContent(
                JsonConvert.SerializeObject(messages),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/message", messagesJson);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentString = await httpResponse.Content.ReadAsStringAsync();
                var parsedContent = JsonConvert.DeserializeObject<string>(contentString);
                return new ServiceResult<string>(parsedContent);
            }
            else
            {
                return new ServiceResult<string>(false, null);
            }
        }
    }
}
