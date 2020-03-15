using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageBoard.Api.Data
{
    public abstract class IdentityEntity
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }
    }
}