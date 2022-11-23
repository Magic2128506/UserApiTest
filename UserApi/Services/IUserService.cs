using System.Threading.Tasks;
using System.Threading;
using UserApi.Contract.Entities;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task Put(UserViewModel user, CancellationToken cancellationToken);
    }
}
