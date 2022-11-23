using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using UserApi.Contract.Requests.OrganizationRequests.AddUserToOrganization;
using UserApi.Contract.Requests.OrganizationRequests.GetUsers;

namespace OrganizationApi.Controllers
{
    [ApiController]
    [Route("Organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator) => _mediator = mediator;

        [HttpPost("{id}/AddUser")]
        public async Task<IActionResult> AddUser([FromRoute] Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var command = new AddUserToOrganizationCommand()
            {
                OrganizationId = id,
                UserId = userId,
            };

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("GetUsers")]
        public async Task<GetUsersQueryResponse> GetUsers(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);

            return response;
        }
    }
}
