using Microsoft.EntityFrameworkCore;
using SaveUp.Web.API.Entities;

namespace SaveUp.Web.API.Tests.TestHelper
{
    public class SaveUpDbContextBuilder
    {
        public TestIdentity TestIdentity = new TestIdentity();

        private readonly List<Entrie> entries = new List<Entrie>();
        private readonly List<User> users = new List<User>();

        public SaveUpDbContextBuilder() { }

        public SaveUpDbContextBuilder SetTestIdentity(TestIdentity testIdentity)
        {
            this.TestIdentity = testIdentity;
            return this;
        }

        public SaveUpDbContextBuilder AddEntries(IEnumerable<Entrie> entries)
        {
            this.entries.AddRange(entries);
            return this;
        }

        public SaveUpDbContextBuilder AddUsers(params User[] users)
        {
            this.users.AddRange(users);
            return this;
        }

        public SaveUpDbContext Build()
        {
            var contextOptions = new DbContextOptionsBuilder<SaveUpDbContext>()
                .UseInMemoryDatabase("save_up").EnableServiceProviderCaching(false).Options;

            var context = new SaveUpDbContext(contextOptions, this.TestIdentity);

            context.Entries.AddRange(this.entries);
            context.Users.AddRange(this.users);
            context.SaveChanges();

            return context;
        }
    }
}
