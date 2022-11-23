using AutoMapper;
using UserApi.Contract.Entities;
using UserApi.Contract.Requests.OrganizationRequests.GetUsers;

namespace UserApi.Contract.Configuration
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, GetUsersQueryResponseItem>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
