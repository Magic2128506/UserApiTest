using System.Threading.Tasks;
using MassTransit;
using OrganizationApi.Data;
using UserApi.Contract;
using UserApi.Contract.Entities;

namespace OrganizationApi.Consumers
{
    public class UserMessageConsumer : IConsumer<IUserMessage>
    {
        private readonly IApplicationDbContext _dbContext;

        public UserMessageConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<IUserMessage> context)
        {
            try
            {
                var userModel = context.Message.UserViewModel;
                var user = new User
                {
                    Name = userModel.Name,
                    Email = userModel.Email,
                    MiddleName = userModel.MiddleName,
                    SurName = userModel.SurName,
                    PhoneNumber = userModel.PhoneNumber
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                await context.RespondAsync(context.Message.MessageId);
            }
            catch
            {
                // log
            }
        }
    }
}
