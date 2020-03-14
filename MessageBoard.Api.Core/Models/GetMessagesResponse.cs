using System.Collections.Generic;

namespace MessageBoard.Api.Core.Models
{
    public class GetMessagesResponse
    {
        public GetMessagesResponse(IList<Message> messages)
        {
            Messages = messages;
        }

        public IList<Message> Messages { get; set; }
    }
}