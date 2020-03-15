using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessageBoard.Api.Core.Mappers;
using MessageBoard.Api.Core.Models;
using MessageBoard.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Api.Core.Services
{
    public class MessageBoardService : IMessageBoardService
    {
        private readonly IMessageMapper _mapper;
        private readonly MessageBoardDbContext _dbContext;

        public MessageBoardService(IMessageMapper mapper, MessageBoardDbContext dbContext)
        {
            if(mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            if(dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<GetMessagesResponse> GetAsync()
        {
            var messages = await _dbContext.Messages.ToListAsync();
            return _mapper.ToGetMessagesResponse(messages);
        }

        public async Task SendAsync(SendMessageRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException("request");
            }

            var messageDb = _mapper.ToMessageDbModel(request);
            messageDb.Created = DateTime.UtcNow;

            _dbContext.Add(messageDb);
            await _dbContext.SaveChangesAsync();
        }
    }
}