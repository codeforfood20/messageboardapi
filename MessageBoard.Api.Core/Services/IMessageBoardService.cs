using MessageBoard.Api.Core.Models;
using System.Threading.Tasks;

namespace MessageBoard.Api.Core.Services
{
    public interface IMessageBoardService
    {
        Task<GetMessagesResponse> GetAsync();

        Task SendAsync(SendMessageRequest request);        
    }
}