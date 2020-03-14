using System;

namespace MessageBoard.Api.Data
{
    public class Message : IdentityEntity
    {
       public string User { get; set; }

       public string Content { get; set; }

       public DateTime Created { get; set; }
    }
}