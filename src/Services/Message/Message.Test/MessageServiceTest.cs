using Message.Api.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Message.Test
{
    public class MessageServiceTest
    {
        private readonly MessageService _messageService = new MessageService();

        [Fact]
        public void Messages_incorrect_amount_of_values()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "", "este", "es", "un", "mensaje" };
            var secondMessage = new List<string>() { "este", "", "un", "mensaje" };
            var thirdMessage = new List<string>() { "", "", "es", "", "mensaje" };
            messages.Add(firstMessage);
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.False(message.Ok);
            Assert.Null(message.Result);
        }

        [Fact]
        public void Messages_correct_amount_of_values_not_enough_words()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "este", "", "", "mensaje", "" };
            var secondMessage = new List<string>() { "", "es", "", "", "" };
            var thirdMessage = new List<string>() { "este", "", "un", "", "" };
            messages.Add(firstMessage);
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.False(message.Ok);
            Assert.Null(message.Result);
        }

        [Fact]
        public void Messages_correct_amount_of_values_with_error()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "este", "", "", "mensaje", "" };
            var secondMessage = new List<string>() { "", "es", "", "", "secreto" };
            var thirdMessage = new List<string>() { "este", "", "un", "", "error" };
            messages.Add(firstMessage);
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.False(message.Ok);
            Assert.Null(message.Result);
        }

        [Fact]
        public void Messages_correct_amount_of_values()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "este", "", "", "mensaje", "" };
            var secondMessage = new List<string>() { "", "es", "", "", "secreto" };
            var thirdMessage = new List<string>() { "este", "", "un", "", "" };
            messages.Add(firstMessage); 
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.True(message.Ok);
            Assert.Equal("este es un mensaje secreto", message.Result);
        }

        [Fact]
        public void Messages_incorrect_amount_of_values_enough_words()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "este", "", "", "mensaje", "" };
            var secondMessage = new List<string>() { "", "es", "", "", "secreto" };
            var thirdMessage = new List<string>() { "este", "", "un", "" };
            messages.Add(firstMessage);
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.True(message.Ok);
            Assert.Equal("este es un mensaje secreto", message.Result);
        }

        [Fact]
        public void Messages_incorrect_amount_of_values_enough_words_phased()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "un", "", "secreto", "este" };
            var secondMessage = new List<string>() { "es", "", "", "secreto", "este" };
            var thirdMessage = new List<string>() { "mensaje", "", "este", "", "un" };
            messages.Add(firstMessage);
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.True(message.Ok);
            Assert.Equal("este es un mensaje secreto", message.Result);
        }

        [Fact]
        public void Messages_correct_amount_of_values_enough_words_phased()
        {
            var messages = new List<List<string>>();
            var firstMessage = new List<string>() { "un", "", "secreto", "este", "" };
            var secondMessage = new List<string>() { "es", "", "", "secreto", "este" };
            var thirdMessage = new List<string>() { "mensaje", "", "este", "", "un" };
            messages.Add(firstMessage);
            messages.Add(secondMessage);
            messages.Add(thirdMessage);

            var message = _messageService.GetMessage(messages);

            Assert.True(message.Ok);
            Assert.Equal("este es un mensaje secreto", message.Result);
        }
    }
}
