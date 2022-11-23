using MediatR;
using UserApi.Contract.Entities;

namespace UserApi.Contract.Requests.UserRequests
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public UserViewModel User { get; set; }
    }
}
