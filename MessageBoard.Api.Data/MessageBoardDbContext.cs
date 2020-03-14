using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Api.Data
{
    public class MessageBoardDbContext : DbContext
    {
        public MessageBoardDbContext(DbContextOptions<MessageBoardDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}