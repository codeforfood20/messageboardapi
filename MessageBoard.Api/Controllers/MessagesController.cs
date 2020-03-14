using System;
using System.Collections.Generic;
using System.Linq;
using MessageBoard.Api.Core.Models;
using MessageBoard.Api.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.Api.Controllers
{
    [Route("api/messageboard/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessageBoardService _messageBoard;

        public MessagesController(IMessageBoardService messageBoard)
        {
            if(messageBoard == null)
            {
                throw new ArgumentNullException("messageBoard");
            }

            _messageBoard = messageBoard;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var response = _messageBoard.Get();

                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log exception

                return StatusCode(500);
            }           
        }

        [HttpPost]
        public IActionResult Create([FromBody] SendMessageRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _messageBoard.Send(request);

                    return Ok();
                }
                catch(Exception ex)
                {
                    // Log exception

                    return StatusCode(500);
                }             
            }

            return BadRequest(ModelState);
        }
    }
}