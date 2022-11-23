using System;
using Microsoft.EntityFrameworkCore;
using UserApi.Contract.Entities;

namespace OrganizationApi.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());

            modelBuilder.Entity<Organization>().HasData(
                new Organization { Id = new Guid("66bb9791-09ee-4891-a543-e067f97583f1"), Name= "Лахта SPb"},
                new Organization { Id = new Guid("99c693a9-0935-4341-9d7e-cef3ef9ae751"), Name= "etprf"},
                new Organization { Id = new Guid("5e0072e4-9266-4e4b-b537-9ac36cb8b1aa"), Name= "Innostage"},
                new Organization { Id = new Guid("56289e2f-c94f-4e04-af68-344068a36439"), Name= "Ak Bars"},
                new Organization { Id = new Guid("505c1939-1d60-4460-9500-6fb4423daa05"), Name= "Abdt"});
        }
    }
}
