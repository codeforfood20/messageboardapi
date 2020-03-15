using MessageBoard.Api.Core.Mappers;
using MessageBoard.Api.Core.Models;
using MessageBoard.Api.Core.Services;
using MessageBoard.Api.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MessageDb = MessageBoard.Api.Data.Message;

namespace MessageBoard.Api.Tests.Core.Services
{
    public class MessageBoardServiceTests
    {
        [Fact]
        public void MessageBoardService_Constructor_ThrowsNullException_When_MessageMapper_Is_Null()
        {
            // Arrange
            IMessageMapper mapper = null;
            var options = new DbContextOptions<MessageBoardDbContext>();
            var dbContext = new MessageBoardDbContext(options);
          
            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new MessageBoardService(mapper, dbContext));
        }

        [Fact]
        public void MessageBoardService_Constructor_ThrowsNullException_When_MessageBoardDbContext_Is_Null()
        {
            // Arrange
            var mapper = new Mock<IMessageMapper>();
            //var options = new Mock<DbContextOptions<MessageBoardDbContext>>();
            //var dbContext = new MessageBoardDbContext(options.Object);
            MessageBoardDbContext dbContext = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new MessageBoardService(mapper.Object, dbContext));
        }

        [Fact]
        public async Task MessageBoardService_GetAsync_Returns_Two_Messages()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MessageBoardDbContext>()
                        .UseInMemoryDatabase("MessageBoardDB")
                        .Options;         
            
            using(var dbContext = new MessageBoardDbContext(options))
            {
                // Clear existing messages
                dbContext.Messages.RemoveRange(dbContext.Messages);
                await dbContext.SaveChangesAsync();

                // Initialize new messages
                dbContext.Messages.Add(new MessageDb { User = "Rick", Content = "Hello Tom.", Created = DateTime.Now.AddMinutes(5) });
                dbContext.Messages.Add(new MessageDb { User = "Tom", Content = "Hi, Rick.", Created = DateTime.Now.AddMinutes(5) });
                await dbContext.SaveChangesAsync();
            };

            var mapper = new MessageMapper();

            using (var dbContext = new MessageBoardDbContext(options))
            {
                var service = new MessageBoardService(mapper, dbContext);

                // Act
                var result = await service.GetAsync();

                // Assert
                Assert.Equal(2, result.Messages.Count());
            };
        }

        [Fact]
        public async Task MessageBoardService_SendAsync_Throws_Exception_When_SendMessageRequest_Is_Null()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MessageBoardDbContext>()
                        .UseInMemoryDatabase("MessageBoardDB")
                        .Options;

            using (var dbContext = new MessageBoardDbContext(options))
            {
                // Clear existing messages
                dbContext.Messages.RemoveRange(dbContext.Messages);
                await dbContext.SaveChangesAsync();

                // Initialize new messages
                dbContext.Messages.Add(new MessageDb { User = "Rick", Content = "Hello Tom.", Created = DateTime.Now.AddMinutes(5) });
                dbContext.Messages.Add(new MessageDb { User = "Tom", Content = "Hi, Rick.", Created = DateTime.Now.AddMinutes(5) });
                await dbContext.SaveChangesAsync();
            };

            SendMessageRequest request = null;
            var mapper = new MessageMapper();

            using (var dbContext = new MessageBoardDbContext(options))
            {
                var service = new MessageBoardService(mapper, dbContext);

                // Act and Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => service.SendAsync(request));
            };
        }

        [Fact]
        public async Task MessageBoardService_SendAsync_Returns_ThreeMessages()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MessageBoardDbContext>()
                        .UseInMemoryDatabase("MessageBoardDB")
                        .Options;

            using (var dbContext = new MessageBoardDbContext(options))
            {
                // Clear existing messages
                dbContext.Messages.RemoveRange(dbContext.Messages);
                await dbContext.SaveChangesAsync();

                // Initialize new messages
                dbContext.Messages.Add(new MessageDb { User = "Rick", Content = "Hello Tom.", Created = DateTime.Now.AddMinutes(5) });
                dbContext.Messages.Add(new MessageDb { User = "Tom", Content = "Hi, Rick.", Created = DateTime.Now.AddMinutes(5) });
                await dbContext.SaveChangesAsync();                
            };

            var request = new SendMessageRequest
            {
                User = "Rick",
                Message = "It works."
            };
            var mapper = new MessageMapper();

            using (var dbContext = new MessageBoardDbContext(options))
            {
                var service = new MessageBoardService(mapper, dbContext);

                // Act
                await service.SendAsync(request);
                var response = await service.GetAsync();

                // Assert
                Assert.Equal(3, response.Messages.Count);                
            };
        }
    }
}