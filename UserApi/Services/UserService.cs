using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using System;
using Microsoft.Extensions.Logging;
using UserApi.Contract;
using UserApi.Contract.Entities;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IPublishEndpoint _endpoint;

        public UserService(ILogger<UserService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        public async Task Put(UserViewModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _endpoint.Publish<IUserMessage>(new UserMessage()
                {
                    MessageId = new Guid(),
                    UserViewModel = user,
                    CreationDate = DateTime.Now
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
