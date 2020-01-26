using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Tests
{
    public class MyWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UserDbContext>));

                if (descriptor != null)
                {
                    // Remove the current description pointing to the application database.
                    // We will be registering an in memory db context.
                    services.Remove(descriptor);
                }

                services.AddDbContext<UserDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestingDb");
                });

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    // Use a scope to retrieve the db context as it is registered as scoped service,
                    // and initialize the schema and the test data.
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<UserDbContext>();

                    db.Database.EnsureCreated();
                    InitializeDbForTests(db);
                }
            });
        }

        public static void InitializeDbForTests(UserDbContext db)
        {
            db.Users.Add(Data.User1);
            db.SaveChanges();
        }
    }
}