using UserApi.Contract.Entities;

namespace UserApi.Contract.Requests.OrganizationRequests.GetUsers
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class GetUsersQueryResponseItem
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
