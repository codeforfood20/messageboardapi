using MessageBoard.Api.Core.Models;
using System;
using System.Collections.Generic;

namespace MessageBoard.Api.Core.Services
{
    public class MessageBoardService : IMessageBoardService
    {
        public GetMessagesResponse Get()
        {
            var messages = new List<Message>
            {
                new Message { User = "Tony", Content = "Hello Rick.", Created = DateTime.Now.AddMinutes(10)},
                new Message { User = "Rick", Content = "Hello Tony.", Created = DateTime.Now.AddMinutes(5)},
                new Message { User = "Tony", Content = "It works.", Created = DateTime.Now },
            };

            return new GetMessagesResponse(messages);
        }

        public void Send(SendMessageRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException("request");
            }


        }
    }
}