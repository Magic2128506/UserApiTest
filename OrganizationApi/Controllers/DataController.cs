using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrganizationApi.Data;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using UserApi.Contract.Entities;

namespace OrganizationApi.Controllers
{
    /// <summary>
    /// Контроллер для просмотра id пользователей и организаций (костыль)
    /// </summary>
    [ApiController]
    [Route("Data")]
    public class DataController : ControllerBase
    {
        private readonly IApplicationDbContext _dbContext;

        public DataController(IApplicationDbContext dbContext) => _dbContext = dbContext;

        [HttpGet("GetUsers")]
        public async Task<List<User>> GetUsers(CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.ToListAsync(cancellationToken);

            return users;
        }

        [HttpGet("GetOrganizations")]
        public async Task<List<Organization>> GetOrganizations(CancellationToken cancellationToken)
        {
            var organizations = await _dbContext.Organizations.ToListAsync(cancellationToken);

            return organizations;
        }
    }
}
