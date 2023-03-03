namespace SaveUp.Web.API.Services
{
    public interface ITokenService
    {
        string CreateToken(int id, string username);
    }
}
