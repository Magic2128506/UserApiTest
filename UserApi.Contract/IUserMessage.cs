using System;
using UserApi.Contract.Entities;

namespace UserApi.Contract
{
    public interface IUserMessage
    {
        Guid MessageId { get; set; }
        UserViewModel UserViewModel { get; set; }
        DateTime CreationDate { get; set; }
    }
}
