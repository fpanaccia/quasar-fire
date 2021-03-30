using Newtonsoft.Json;
using Radio.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Radio.Test
{
    public class TopSecret
    {
        public HttpClient _client;

        public TopSecret()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"http://localhost/");
        }

        [Fact]
        public async Task Basic_example()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "skywalker",
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/topsecret", messageJson);

            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }

        [Fact]
        public async Task Basic_example_100()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "skywalker",
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var tasks = new List<Task<HttpResponseMessage>>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(_client.PostAsync($"/topsecret", messageJson));
            }
            await Task.WhenAll(tasks);

            Assert.True(tasks.TrueForAll(x => x.Result.IsSuccessStatusCode));
        }

        [Fact]
        public async Task Basic_example_duplicated()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "skywalker",
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/topsecret", messageJson);

            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }

        [Fact]
        public async Task Basic_example_phased()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "skywalker",
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/topsecret", messageJson);

            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }

        [Fact]
        public async Task Basic_example_error_incomplete()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "skywalker",
                Distance = 115.5,
                Message = new string[] { "", "es", "", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/topsecret", messageJson);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Basic_example_error_missing()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/topsecret", messageJson);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Example_full_message_one_empty()
        {
            var message = new TopSecretMessage();
            var satellites = new List<SatelliteMessage>();
            satellites.Add(new SatelliteMessage()
            {
                Name = "kenobi",
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "skywalker",
                Distance = 115.5,
                Message = new string[] { "", "es", "un", "", "secreto" }
            });

            satellites.Add(new SatelliteMessage()
            {
                Name = "sato",
                Distance = 142.7,
                Message = new string[] { "", "", "", "", "" }
            });
            message.Satellites = satellites.ToArray();

            var messageJson = new StringContent(
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await _client.PostAsync($"/topsecret", messageJson);

            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }
    }
}
