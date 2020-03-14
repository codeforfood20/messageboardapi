using MessageBoard.Api.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageBoard.Api.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, string connectionString)
        {
            if(services == null)
            {
                throw new ArgumentNullException();
            }

            return services.AddDbContext<MessageBoardDbContext>(options => 
                options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("MessageBoard.Api.Data"))
                );
        }
    }
}