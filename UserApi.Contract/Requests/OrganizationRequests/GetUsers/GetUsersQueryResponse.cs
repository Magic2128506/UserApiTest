using System.Collections.Generic;

namespace UserApi.Contract.Requests.OrganizationRequests.GetUsers
{
    /// <summary>
    /// Ответ для <see cref="GetUsersQuery"/>
    /// </summary>
    public class GetUsersQueryResponse
    {
        /// <summary>
        /// Ответ для <see cref="GetUsersQuery"/>
        /// </summary>
        /// <param name="data">Список пользователей</param>
        /// <param name="count">Количество элементов, соответствующих фильтрации</param>
        public GetUsersQueryResponse(List<GetUsersQueryResponseItem> data, int count)
        {
            Data = data;
            Count = count;
        }

        /// <summary>
        /// Список пользователей
        /// </summary>
        public List<GetUsersQueryResponseItem> Data { get; set; }

        /// <summary>
        /// Количество элементов, соответствующих фильтрации
        /// </summary>
        public int Count { get; set; }
    }
}
