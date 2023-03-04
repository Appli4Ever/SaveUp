using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SaveUp.Web.API.Services
{
    public class TokenService : ITokenService
    {
        //Injecting IConfiguration into this class in order to read Token Key from the configuration file
        private readonly SymmetricSecurityKey key;
        public TokenService(IConfiguration config)
        {
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        }

        public string CreateToken(int id, string username)
        {
            //Creating Claims. You can add more information in these claims. For example email id.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim("TenantId", id.ToString(CultureInfo.InvariantCulture))
            };

            //Creating credentials. Specifying which type of Security Algorithm we are using
            var creds = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha512Signature);

            //Creating Token description
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Finally returning the created token
            return tokenHandler.WriteToken(token);
        }
    }
}
