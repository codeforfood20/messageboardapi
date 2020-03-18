using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoard.Api.Core.Models;
using MessageBoard.Api.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageBoard.Api.Controllers
{
    [Route("api/messageboard/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageBoardService _messageBoard;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessageBoardService messageBoard, ILogger<MessagesController> logger)
        {
            if(messageBoard == null)
            {
                throw new ArgumentNullException("messageBoard");
            }

            if(logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _messageBoard = messageBoard;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Getting messages");
                var response = await _messageBoard.GetAsync();
                    
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500);
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SendMessageRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Sending message...");
                    await _messageBoard.SendAsync(request);

                    return Ok();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);

                    return StatusCode(500);
                }             
            }

            return BadRequest(ModelState);
        }
    }
}