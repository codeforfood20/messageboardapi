using System;

namespace MessageBoard.Api.Core.Models
{
    public class Message
    {
        public string User { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}