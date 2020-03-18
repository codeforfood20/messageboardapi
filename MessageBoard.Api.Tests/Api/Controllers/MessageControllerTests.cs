using MessageBoard.Api.Controllers;
using MessageBoard.Api.Core.Models;
using MessageBoard.Api.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public void MessagesController_Constructor_ThrowsNullException_When_MessageBoardService_Is_Null()
        {
            // Arrange
            IMessageBoardService service = null;
            ILogger<MessagesController> logger = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new MessagesController(service, logger));
        }

        [Fact]
        public void MessagesController_Constructor_ThrowsNullException_When_Logger_Is_Null()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            ILogger<MessagesController> logger = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new MessagesController(service.Object, logger));
        }

        [Fact]
        public async Task MessagesController_Create_Returns_InternalServerError_When_MessageBoardService_Throws_Exception()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.SendAsync(It.IsAny<SendMessageRequest>()))
                                .Throws<Exception>();
            var logger = new Mock<ILogger<MessagesController>>();

            var request = new SendMessageRequest
            {
                User = "Tom",
                Message = "Hello, Rick"
            };

            var controller = new MessagesController(service.Object, logger.Object);

            // Act
            var result = await controller.Post(request);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, (result as StatusCodeResult).StatusCode);
        }

        [Fact]
        public async Task MessagesController_Create_Returns_OKResult_When_MessageBoardService_Is_Valid()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.SendAsync(It.IsAny<SendMessageRequest>()))
                                .Verifiable();
            var logger = new Mock<ILogger<MessagesController>>();

            var request = new SendMessageRequest
            {
                User = "Tom",
                Message = "Hello, Rick"
            };

            var controller = new MessagesController(service.Object, logger.Object);

            // Act
            var result = await controller.Post(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task MessagesController_Get_Returns_InternalServerError_When_MessageBoardService_Throws_Exception()
        {
            // Arrange
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.GetAsync()).ThrowsAsync(new Exception());

            var logger = new Mock<ILogger<MessagesController>>();

            var controller = new MessagesController(service.Object, logger.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, (result as StatusCodeResult).StatusCode);
        }

        [Fact]
        public async Task MessagesController_Get_Returns_OKObjectResult_When_MessageBoardService_Is_Valid()
        {
            // Arrange            
            var response = new GetMessagesResponse(new List<Message>
            {
                new Message { User = "Rick", Content = "Hello Tom.", Created = DateTime.Now.AddMinutes(5)},
                new Message { User = "Tom", Content = "It works.", Created = DateTime.Now }
            });
            var service = new Mock<IMessageBoardService>();
            service.Setup(s => s.GetAsync()).ReturnsAsync(response);

            var logger = new Mock<ILogger<MessagesController>>();

            var controller = new MessagesController(service.Object, logger.Object);

            // Act
            var result = await controller.Get();           

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(2, ((result as OkObjectResult).Value as GetMessagesResponse).Messages.Count);
        }
    }
}