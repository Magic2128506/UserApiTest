using System;

namespace UserApi.Contract.Entities
{
    public class User : UserViewModel
    {
        public Guid Id { get; set; }
        public Guid? OrganizationId { get; set; }
    }
}
