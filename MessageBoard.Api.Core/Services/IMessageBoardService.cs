using MessageBoard.Api.Core.Models;

namespace MessageBoard.Api.Core.Services
{
    public interface IMessageBoardService
    {
        GetMessagesResponse Get();

        void Send(SendMessageRequest request);        
    }
}