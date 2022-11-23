using MediatR;
using System;

namespace UserApi.Contract.Requests.OrganizationRequests.GetUsers
{
    /// <summary>
    /// Запрос пользователей по организации
    /// </summary>
    public class GetUsersQuery : IRequest<GetUsersQueryResponse>
    {
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Номер страницы, начиная с 1
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; }
    }
}
