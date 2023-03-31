using SaveUp.Web.API.Services;

namespace SaveUp.Web.API.Tests.MockServices;

public class MockTokenService : ITokenService
{
    public string CreateToken(int id, string username)
    {
        return $"{id}, {username}";
    }
}