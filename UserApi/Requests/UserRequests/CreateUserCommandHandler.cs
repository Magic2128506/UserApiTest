using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UserApi.Contract.Requests.UserRequests;
using UserApi.Controllers;
using UserApi.Services;

namespace UserApi.Requests.UserRequests
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public CreateUserCommandHandler(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.Put(request.User, cancellationToken);
            _logger.Log(LogLevel.Information, $"Импорт пользователя {request.User}");

            return default;
        }
    }
}
