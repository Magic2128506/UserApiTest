using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrganizationApi.Data;
using OrganizationApi.Requests.OrganizationRequests;
using UserApi.Contract.Entities;
using UserApi.Contract.Requests.OrganizationRequests.AddUserToOrganization;
using Xunit;
using Xunit.Abstractions;

namespace OrganizationApi.UnitTest.Requests.OrganizationRequests
{
    public class AddUserToOrganizationCommandHandlerTest : UnitTestBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Organization _organization;
        private readonly User _user;

        public AddUserToOrganizationCommandHandlerTest(ITestOutputHelper output) : base(output)
        {
            _organization = new Organization
            {
                Id = Guid.NewGuid(),
                Name = "Lakhta"
            };
            _user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Тимур",
                SurName = "Насибуллин",
                PhoneNumber = "777",
                MiddleName = "Радимович",
                Email = "some@mail.com"
            };
            _dbContext = CreateInMemoryContext(dbSeeder: x =>
            {
                x.Add(_user);
                x.Add(_organization);
            });
        }

        [Fact]
        public async Task Handle_ShouldAddUser()
        {
            var handler = new AddUserToOrganizationCommandHandler(_dbContext);
            var request = new AddUserToOrganizationCommand
            {
                UserId = _user.Id,
                OrganizationId = _organization.Id
            };
            await handler.Handle(request, default);

            var userFromDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _user.Id);

            Assert.NotNull(userFromDb?.OrganizationId);
            Assert.Equal(userFromDb!.OrganizationId, _organization.Id);
        }
    }
}
