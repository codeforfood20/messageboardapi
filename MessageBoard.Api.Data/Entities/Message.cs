using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageBoard.Api.Data
{
    public class Message : IdentityEntity
    {
       [Required]
       [Column(TypeName = "varchar(25)")]
        public string User { get; set; }

       [Required]
       [Column(TypeName = "varchar(255)")]
       public string Content { get; set; }

       [Required]
       public DateTime Created { get; set; }
    }
}