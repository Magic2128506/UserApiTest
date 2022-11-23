using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganizationApi.Data;
using UserApi.Contract.Requests.OrganizationRequests.AddUserToOrganization;

namespace OrganizationApi.Requests.OrganizationRequests
{
    public class AddUserToOrganizationCommandHandler : IRequestHandler<AddUserToOrganizationCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddUserToOrganizationCommandHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(AddUserToOrganizationCommand request, CancellationToken cancellationToken)
        {
            if (request?.UserId == default || request.OrganizationId == default)
                throw new ValidationException($"Переданы некорректные данные!");

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
               throw new ValidationException($"Не найден пользователь с id {request.UserId}");

            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(x => x.Id == request.OrganizationId, cancellationToken);

            if (organization == null)
                throw new ValidationException($"Не найдена организация с id {request.OrganizationId}");

            user.OrganizationId = organization.Id;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
