using System.Collections.Generic;
using MessageBoard.Api.Core.Models;
using MessageDb = MessageBoard.Api.Data.Message;

namespace MessageBoard.Api.Core.Mappers
{
    public interface IMessageMapper
    {
        MessageDb ToMessageDbModel(SendMessageRequest request);

        Message ToMessageModel(MessageDb messageDb);

        GetMessagesResponse ToGetMessagesResponse(IEnumerable<MessageDb> messages);
    }
}