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
    public class TopSecretSplit
    {
        public HttpClient _client;

        public TopSecretSplit()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"http://localhost/");
        }

        [Fact]
        public async Task Basic_SetAndGet()
        {
            var kenobi = new DistanceWithMessage()
            {
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            };

            var skywalker = new DistanceWithMessage()
            {
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            };

            var sato = new DistanceWithMessage()
            {
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            };

            var kenobiJson = new StringContent(JsonConvert.SerializeObject(kenobi), Encoding.UTF8, "application/json");
            var skywalkerJson = new StringContent(JsonConvert.SerializeObject(skywalker), Encoding.UTF8, "application/json");
            var satoJson = new StringContent(JsonConvert.SerializeObject(sato), Encoding.UTF8, "application/json");

            var kenobiHttpResponse = await _client.PostAsync($"/topsecret_split/kenobi", kenobiJson);
            var skywalkerHttpResponse = await _client.PostAsync($"/topsecret_split/skywalker", skywalkerJson);
            var satoHttpResponse = await _client.PostAsync($"/topsecret_split/sato", satoJson);
            Assert.Equal(System.Net.HttpStatusCode.OK, kenobiHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, skywalkerHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, satoHttpResponse.StatusCode);

            var httpResponse = await _client.GetAsync($"/topsecret_split");
            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }

        [Fact]
        public async Task Basic_example_duplicated()
        {
            var kenobi = new DistanceWithMessage()
            {
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            };

            var skywalker = new DistanceWithMessage()
            {
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            };

            var sato = new DistanceWithMessage()
            {
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            };

            var sato2 = new DistanceWithMessage()
            {
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            };

            var kenobiJson = new StringContent(JsonConvert.SerializeObject(kenobi), Encoding.UTF8, "application/json");
            var skywalkerJson = new StringContent(JsonConvert.SerializeObject(skywalker), Encoding.UTF8, "application/json");
            var satoJson = new StringContent(JsonConvert.SerializeObject(sato), Encoding.UTF8, "application/json");
            var sato2Json = new StringContent(JsonConvert.SerializeObject(sato), Encoding.UTF8, "application/json");

            var kenobiHttpResponse = await _client.PostAsync($"/topsecret_split/kenobi", kenobiJson);
            var skywalkerHttpResponse = await _client.PostAsync($"/topsecret_split/skywalker", skywalkerJson);
            var satoHttpResponse = await _client.PostAsync($"/topsecret_split/sato", satoJson);
            var sato2HttpResponse = await _client.PostAsync($"/topsecret_split/sato", sato2Json);
            Assert.Equal(System.Net.HttpStatusCode.OK, kenobiHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, skywalkerHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, satoHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, sato2HttpResponse.StatusCode);

            var httpResponse = await _client.GetAsync($"/topsecret_split");
            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }

        [Fact]
        public async Task Basic_example_phased()
        {
            var kenobi = new DistanceWithMessage()
            {
                Distance = 100,
                Message = new string[] { "un", "", "secreto", "este", "" }
            };

            var skywalker = new DistanceWithMessage()
            {
                Distance = 115.5,
                Message = new string[] { "es", "", "", "secreto", "este" }
            };

            var sato = new DistanceWithMessage()
            {
                Distance = 142.7,
                Message = new string[] { "mensaje", "", "este", "", "un" }
            };

            var kenobiJson = new StringContent(JsonConvert.SerializeObject(kenobi), Encoding.UTF8, "application/json");
            var skywalkerJson = new StringContent(JsonConvert.SerializeObject(skywalker), Encoding.UTF8, "application/json");
            var satoJson = new StringContent(JsonConvert.SerializeObject(sato), Encoding.UTF8, "application/json");

            var kenobiHttpResponse = await _client.PostAsync($"/topsecret_split/kenobi", kenobiJson);
            var skywalkerHttpResponse = await _client.PostAsync($"/topsecret_split/skywalker", skywalkerJson);
            var satoHttpResponse = await _client.PostAsync($"/topsecret_split/sato", satoJson);
            Assert.Equal(System.Net.HttpStatusCode.OK, kenobiHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, skywalkerHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, satoHttpResponse.StatusCode);

            var httpResponse = await _client.GetAsync($"/topsecret_split");
            Assert.Equal(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var parsedContent = JsonConvert.DeserializeObject<DecodedMessage>(contentString);
            Assert.Equal("este es un mensaje secreto", parsedContent.Message);
        }

        [Fact]
        public async Task Basic_example_error_incomplete()
        {
            var kenobi = new DistanceWithMessage()
            {
                Distance = 100,
                Message = new string[] { "este", "", "", "", "" }
            };

            var skywalker = new DistanceWithMessage()
            {
                Distance = 115.5,
                Message = new string[] { "", "es", "", "", "secreto" }
            };

            var sato = new DistanceWithMessage()
            {
                Distance = 142.7,
                Message = new string[] { "este", "", "un", "", "" }
            };

            var kenobiJson = new StringContent(JsonConvert.SerializeObject(kenobi), Encoding.UTF8, "application/json");
            var skywalkerJson = new StringContent(JsonConvert.SerializeObject(skywalker), Encoding.UTF8, "application/json");
            var satoJson = new StringContent(JsonConvert.SerializeObject(sato), Encoding.UTF8, "application/json");

            var kenobiHttpResponse = await _client.PostAsync($"/topsecret_split/kenobi", kenobiJson);
            var skywalkerHttpResponse = await _client.PostAsync($"/topsecret_split/skywalker", skywalkerJson);
            var satoHttpResponse = await _client.PostAsync($"/topsecret_split/sato", satoJson);
            Assert.Equal(System.Net.HttpStatusCode.OK, kenobiHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, skywalkerHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, satoHttpResponse.StatusCode);

            var httpResponse = await _client.GetAsync($"/topsecret_split");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Basic_example_error_missing()
        {
            var kenobi = new DistanceWithMessage()
            {
                Distance = 100,
                Message = new string[] { "", "", "", "", "" }
            };

            var kenobiJson = new StringContent(JsonConvert.SerializeObject(kenobi), Encoding.UTF8, "application/json");

            var kenobiHttpResponse = await _client.PostAsync($"/topsecret_split/kenobi", kenobiJson);
            Assert.Equal(System.Net.HttpStatusCode.OK, kenobiHttpResponse.StatusCode);

            var httpResponse = await _client.GetAsync($"/topsecret_split");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Example_full_message_one_empty()
        {
            var kenobi = new DistanceWithMessage()
            {
                Distance = 100,
                Message = new string[] { "este", "", "", "mensaje", "" }
            };

            var skywalker = new DistanceWithMessage()
            {
                Distance = 115.5,
                Message = new string[] { "", "es", "un", "", "secreto" }
            };

            var sato = new DistanceWithMessage()
            {
                Distance = 142.7,
                Message = new string[] { "", "", "", "", "" }
            };

            var kenobiJson = new StringContent(JsonConvert.SerializeObject(kenobi), Encoding.UTF8, "application/json");
            var skywalkerJson = new StringContent(JsonConvert.SerializeObject(skywalker), Encoding.UTF8, "application/json");
            var satoJson = new StringContent(JsonConvert.SerializeObject(sato), Encoding.UTF8, "application/json");

            var kenobiHttpResponse = await _client.PostAsync($"/topsecret_split/kenobi", kenobiJson);
            var skywalkerHttpResponse = await _client.PostAsync($"/topsecret_split/skywalker", skywalkerJson);
            var satoHttpResponse = await _client.PostAsync($"/topsecret_split/sato", satoJson);
            Assert.Equal(System.Net.HttpStatusCode.OK, kenobiHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, skywalkerHttpResponse.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, satoHttpResponse.StatusCode);
        }
    }
}
