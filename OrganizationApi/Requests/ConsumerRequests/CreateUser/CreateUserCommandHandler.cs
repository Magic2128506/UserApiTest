using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrganizationApi.Data;
using UserApi.Contract.Entities;
using UserApi.Contract.Requests.ConsumerRequests.CreateUser;

namespace OrganizationApi.Requests.ConsumerRequests.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request?.UserViewModel == null)
                throw new ValidationException("Не переданы параметры запроса!");

            _logger.LogInformation($"Создание пользователя {request.UserViewModel}");

            var user = _mapper.Map<User>(request.UserViewModel);

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
