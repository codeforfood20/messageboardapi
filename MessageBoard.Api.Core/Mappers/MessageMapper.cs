using MessageBoard.Api.Core.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using MessageDb = MessageBoard.Api.Data.Message;

namespace MessageBoard.Api.Core.Mappers
{
    public class MessageMapper : IMessageMapper
    {
        public GetMessagesResponse ToGetMessagesResponse(IEnumerable<MessageDb> messageDbs)
        {
            if(messageDbs == null)
            {
                throw new ArgumentNullException("messageDbs");
            }

            var messages = messageDbs.Select(ToMessageModel).ToList();

            return new GetMessagesResponse(messages);
        }

        public MessageDb ToMessageDbModel(SendMessageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return new MessageDb
            {
                User = request.User,
                Content = request.Message
            };
        }

        public Message ToMessageModel(MessageDb messageDb)
        {
            if (messageDb == null)
            {
                throw new ArgumentNullException("messageDb");
            }

            return new Message
            {
                User = messageDb.User,
                Content = messageDb.Content,
                Created = messageDb.Created
            };
        }
    }
}