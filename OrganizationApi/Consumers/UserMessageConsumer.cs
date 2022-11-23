using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using UserApi.Contract;
using UserApi.Contract.Requests.ConsumerRequests.CreateUser;
using IMediator = MediatR.IMediator;

namespace OrganizationApi.Consumers
{
    public class UserMessageConsumer : IConsumer<IUserMessage>
    {
        private readonly IServiceProvider _serviceProvider;

        public UserMessageConsumer(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task Consume(ConsumeContext<IUserMessage> context)
        {
            try
            {
                var mediator = _serviceProvider.GetRequiredService<IMediator>();
                await mediator.Send(
                    new CreateUserCommand
                    {
                        UserViewModel = context.Message.UserViewModel
                    });

                await context.RespondAsync(context.Message.MessageId);
            }
            catch
            {
                // log
            }
        }
    }
}
