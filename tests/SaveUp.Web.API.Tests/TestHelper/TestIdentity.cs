using SaveUp.Web.API.Entities;

namespace SaveUp.Web.API.Tests.TestHelper
{
    public class TestIdentity : IUser
    {
        public int Id { get; } = 1;
        public string Username { get; } = "TestUser";
    }
}
