using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrganizationApi.Data;
using Xunit.Abstractions;

namespace OrganizationApi.UnitTest
{
    public abstract class UnitTestBase : IDisposable
    {
        protected readonly ITestOutputHelper Output;
        protected readonly Stopwatch Stopwatch = Stopwatch.StartNew();

        protected UnitTestBase(ITestOutputHelper output)
        {
            Output = output;
        }

        public virtual void Dispose()
        {
            Stopwatch.Stop();
            Output.WriteLine($"Execution time: {Stopwatch.Elapsed.TotalSeconds}");
        }

        protected ApplicationDbContext CreateInMemoryContext(Action<ApplicationDbContext> dbSeeder = null, string dbName = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                dbSeeder?.Invoke(context);
                context.SaveChangesAsync().GetAwaiter().GetResult();
            }

            // возвращаем чистый контекст для тестов
            return new ApplicationDbContext(options);
        }
    }
}
