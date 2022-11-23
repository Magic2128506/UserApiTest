using Microsoft.EntityFrameworkCore;
using OrganizationApi.Data;
using System.Threading.Tasks;
using OrganizationApi.Requests.ConsumerRequests.CreateUser;
using UserApi.Contract.Requests.ConsumerRequests.CreateUser;
using Xunit;
using Xunit.Abstractions;
using AutoMapper;
using Moq;
using UserApi.Contract.Entities;
using Microsoft.Extensions.Logging;

namespace OrganizationApi.UnitTest.Requests.ConsumerRequests
{
    public class CreateUserCommandHandlerTest : UnitTestBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateUserCommandHandlerTest(ITestOutputHelper output) : base(output)
        {
            _dbContext = CreateInMemoryContext();
        }

        [Fact]
        public async Task Handle_ShouldAddUser()
        {
            var userViewModel = new UserViewModel()
            {
                Name = "111",
                PhoneNumber = "2",
                MiddleName = "3",
                Email = "4@mail.com",
                SurName = "4"
            };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<User>(It.IsAny<UserViewModel>()))
                .Returns((UserViewModel u) =>
                    new User
                    {
                        Name = u.Name,
                        PhoneNumber = u.PhoneNumber,
                        MiddleName = u.MiddleName,
                        Email = u.Email,
                        SurName = u.SurName
                    });

            var handler = new CreateUserCommandHandler(_dbContext, mockMapper.Object, new Mock<ILogger<CreateUserCommandHandler>>().Object);
            var request = new CreateUserCommand { UserViewModel = userViewModel };
            await handler.Handle(request, default);

            var userFromDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == userViewModel.Name);

            Assert.NotNull(userFromDb);
            Assert.Equal(userFromDb.Name, userViewModel.Name);
            Assert.Equal(userFromDb.PhoneNumber, userViewModel.PhoneNumber);
            Assert.Equal(userFromDb.MiddleName, userViewModel.MiddleName);
            Assert.Equal(userFromDb.Email, userViewModel.Email);
            Assert.Equal(userFromDb.SurName, userViewModel.SurName);
        }
    }
}
