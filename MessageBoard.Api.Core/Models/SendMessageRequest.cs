using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Api.Core.Models
{
    public class SendMessageRequest
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Message { get; set; }
    }
}