using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganizationApi.Data;
using UserApi.Contract.Requests.OrganizationRequests.GetUsers;

namespace OrganizationApi.Requests.OrganizationRequests
{
    /// <summary>
    /// Обработчик для <see cref="GetUsersQuery"/>
    /// </summary>
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users
                .Where(x => x.OrganizationId == request.OrganizationId)
                .AsQueryable();
            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => _mapper.Map<GetUsersQueryResponseItem>(x))
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken);

            return new GetUsersQueryResponse(data, count);
        }
    }
}
