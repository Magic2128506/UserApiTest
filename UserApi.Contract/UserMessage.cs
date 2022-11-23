using System;
using UserApi.Contract.Entities;

namespace UserApi.Contract
{
    public class UserMessage : IUserMessage
    {
        public Guid MessageId { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
