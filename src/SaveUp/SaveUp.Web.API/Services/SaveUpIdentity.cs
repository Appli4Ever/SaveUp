using SaveUp.Web.API.Entities;
using System.Globalization;

namespace SaveUp.Web.API.Services;

public class SaveUpIdentity : IUser
{
    public SaveUpIdentity(IHttpContextAccessor accessor)
    {
        if (accessor.HttpContext == null)
        {
            return;
        }

        var claims = accessor.HttpContext.User.Claims.ToList();

        var tenant = claims.FirstOrDefault(t => t.Type == "TenantId")?.Value ?? "0";
        this.Id = int.Parse(tenant, CultureInfo.InvariantCulture);
        this.Username = claims.FirstOrDefault(t => t.Type == "nameid")?.Value ?? "Nicht registriert";
    }

    public int Id { get; }
    public string Username { get; }
}