using Location.Dto;
using System.Collections.Generic;

namespace Message.Api.Services.Interfaces
{
    public interface IMessageService
    {
        ServiceResult<string> GetMessage(List<List<string>> messages);
    }
}