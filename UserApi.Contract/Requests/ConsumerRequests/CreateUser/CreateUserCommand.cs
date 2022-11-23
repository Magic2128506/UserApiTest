using MediatR;
using UserApi.Contract.Entities;

namespace UserApi.Contract.Requests.ConsumerRequests.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public UserViewModel UserViewModel { get; set; }
    }
}
