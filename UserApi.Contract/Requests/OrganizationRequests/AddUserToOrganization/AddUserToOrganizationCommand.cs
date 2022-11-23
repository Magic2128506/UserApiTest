using System;
using MediatR;

namespace UserApi.Contract.Requests.OrganizationRequests.AddUserToOrganization
{
    public class AddUserToOrganizationCommand : IRequest<Unit>
    {
        public Guid OrganizationId { get; set; }

        public Guid UserId { get; set; }
    }
}
