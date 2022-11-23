using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using UserApi.Contract.Entities;

namespace OrganizationApi.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Organization> Organizations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
