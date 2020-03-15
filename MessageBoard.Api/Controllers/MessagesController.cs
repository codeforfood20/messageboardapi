using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _messageBoard.GetAsync();

                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log exception

                return StatusCode(500);
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SendMessageRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _messageBoard.SendAsync(request);

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