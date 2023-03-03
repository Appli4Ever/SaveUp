using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveUp.Models.ViewModels;
using SaveUp.Web.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaveUp.Web.API.Controllers
{
    /// <summary>
    /// Benutzer Controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService">Benutzer Service</param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Erstellt einen Benutzer
        /// (Username muss eindeutig sein)
        /// </summary>
        /// <param name="model">Den zu erstellenden Benutzer</param>
        /// <returns>Username des erstellten Benutzer</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser(UserViewModel model)
        {
            var result = await this.userService.CreateUser(model);

            if (result is null)
            {
                return this.BadRequest("User nicht angelegt");
            }

            return this.Ok(model.Username);
        }

        /// <summary>
        /// Überprüft die Logindaten eines Benutzers
        /// </summary>
        /// <param name="model">Benutzerlogin Daten</param>
        /// <returns>TokenViewModel mit dem Status und Token</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<TokenViewModel>> Login(UserViewModel model)
        {
            var result = await this.userService.CheckLogin(model);

            switch (result.LoginStatus)
            {
                case LoginStatus.Success:
                    return this.Ok(result);
                case LoginStatus.Faild or LoginStatus.Blocked:
                    return this.BadRequest(result);
                default:
                    return this.BadRequest();
            }
        }

        /// <summary>
        /// Ändert das Passwort des angemeldeten Benutzer
        /// </summary>
        /// <param name="model">PasswortViewModel mit doppelter Passwort Eingabe</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(PasswordViewModel model)
        {
            var result = await this.userService.ChangePassword(model);

            if (!result)
            {
                return this.BadRequest("Fehler bei der Passwortänderung");
            }

            return this.Ok();
        }
    }
}
