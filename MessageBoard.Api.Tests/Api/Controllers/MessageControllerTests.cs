using MessageBoard.Api.Controllers;
using MessageBoard.Api.Core.Models;
using MessageBoard.Api.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MessageBoard.Api.Tests.Api.Controllers
{
    public class MessageControllerTests
    {
        [Fact]
        public void MessagesController_Constructor_ThrowsNullException()
        {
            // Arrange
            IMessageBoardService service = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new MessagesController(service));
        }

        [Fact]
        public async Task MessagesController_Create_Returns_InternalServerError()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.SendAsync(It.IsAny<SendMessageRequest>()))
                                .Throws<Exception>();
            var request = new SendMessageRequest
            {
                User = "Tom",
                Message = "Hello, Rick"
            };

            var controller = new MessagesController(service.Object);

            // Act
            var result = await controller.Create(request);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, (result as StatusCodeResult).StatusCode);
        }

        [Fact]
        public async Task MessagesController_Create_Returns_OKResult()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.SendAsync(It.IsAny<SendMessageRequest>()))
                                .Verifiable();
            var request = new SendMessageRequest
            {
                User = "Tom",
                Message = "Hello, Rick"
            };

            var controller = new MessagesController(service.Object);

            // Act
            var result = await controller.Create(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task MessagesController_Get_Returns_InternalServerError()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.GetAsync()).ThrowsAsync(new Exception());
                         
            var controller = new MessagesController(service.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, (result as StatusCodeResult).StatusCode);
        }

        [Fact]
        public async Task MessagesController_Get_Returns_OKObjectResult()
        {
            // Arrange            
            var response = new GetMessagesResponse(new List<Message>
            {
                new Message { User = "Rick", Content = "Hello Tom.", Created = DateTime.Now.AddMinutes(5)},
                new Message { User = "Tom", Content = "It works.", Created = DateTime.Now }
            });
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.GetAsync()).ReturnsAsync(response);

            var controller = new MessagesController(service.Object);

            // Act
            var result = await controller.Get();           

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(2, ((result as OkObjectResult).Value as GetMessagesResponse).Messages.Count);
        }
    }
}