using Location.Dto;
using Message.Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly string[] _words = new string[] { "este", "es", "un", "mensaje", "secreto" };

        public ServiceResult<string> GetMessage(List<List<string>> messages)
        {
            if (messages == null || (messages != null && (messages.Count != 3 || !messages.Any())))
            {
                return new ServiceResult<string>(false, null);
            }

            if (messages.Any(x => x.Count < _words.Length))
            {
                messages = FillUpMessages(messages);
            }
            
            if (messages.Any(x => x.Select((value, idx) => new { value, idx }).Any(y => y.value != _words[y.idx] && !string.IsNullOrWhiteSpace(y.value))))
            {
                messages = ReOrderMessages(messages);
            }

            var transposedMessage = TransposeMessage(messages);
            if (transposedMessage.Any(x => x.All(y => string.IsNullOrWhiteSpace(y)) || x.Where(y => !string.IsNullOrWhiteSpace(y)).Distinct().Count() > 1))
            {
                return new ServiceResult<string>(false, null);
            }

            var messageArray = transposedMessage.Select(x => x.Where(y => !string.IsNullOrWhiteSpace(y)).Distinct().FirstOrDefault());
            var message = string.Join(" ", messageArray);
            return new ServiceResult<string>(message);
        }

        private List<List<string>> TransposeMessage(List<List<string>> messages)
        {
            return messages.SelectMany(inner => inner.Select((item, index) => new { item, index }))
                           .GroupBy(i => i.index, i => i.item)
                           .Select(g => g.ToList())
                           .ToList();
        }

        private List<List<string>> ReOrderMessages(List<List<string>> messages)
        {
            var ret = new List<List<string>>();

            for (int i = 0; i < messages.Count(); i++)
            {
                var message = messages[i];
                var splitIdx = 0;
                for (int j = 0; j < _words.Length; j++)
                {
                    var word = _words[j];
                    if (message[j] != word && !string.IsNullOrWhiteSpace(message[j]) && message.Any(x => x == word))
                    {
                        splitIdx = message.FindIndex(x => x == word);
                        break;
                    }
                }

                if (splitIdx == 0)
                {
                    ret.Add(message);
                }
                else
                {
                    var newMessage = message.Skip(splitIdx).ToList();
                    var messageEnd = message.Take(splitIdx).ToList();
                    newMessage.AddRange(messageEnd);
                    ret.Add(newMessage);
                }
            }

            return ret;
        }

        private List<List<string>> FillUpMessages(List<List<string>> messages)
        {
            var ret = new List<List<string>>();

            foreach (var message in messages)
            {
                while(message.Count < _words.Length)
                {
                    message.Add("");
                }

                ret.Add(message);
            }

            return ret;
        }
    }
}
