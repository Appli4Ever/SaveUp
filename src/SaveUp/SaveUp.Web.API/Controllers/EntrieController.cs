using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveUp.Models.ViewModels;
using SaveUp.Web.API.Services;

namespace SaveUp.Web.API.Controllers
{
    /// <summary>
    /// Eintrag Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EntrieController : ControllerBase
    {
        private readonly IEntrieService entrieService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entrieService">Entrie Service</param>
        public EntrieController(IEntrieService entrieService)
        {
            this.entrieService = entrieService;
        }

        /// <summary>
        /// Gibt alle Einträge zurück
        /// </summary>
        /// <returns>Liste aller Einträge</returns>
        [HttpGet]
        public async Task<ActionResult<List<EntrieViewModel>>> All()
        {
            return this.Ok(await this.entrieService.GetAll());
        }

        /// <summary>
        /// Löscht den Eintrag mit der Id
        /// </summary>
        /// <param name="id">Id des Eintrag</param>
        /// <returns>Der gelöschte Eintrag</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EntrieViewModel>> Delete(int id)
        {
            var result = await this.entrieService.Delete(id);

            if (result is null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Löscht alle Einträge mit der ID
        /// </summary>
        /// <param name="idList">Liste der Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<EntrieViewModel>>> Delete([FromBody] List<int> idList)
        {
            var result = await this.entrieService.DeleteRange(idList);

            if (result is null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Erstellt einen Eintrag
        /// </summary>
        /// <param name="model">Der zu erstellende Eintrag</param>
        /// <returns>Der erstellte Eintrag</returns>
        [HttpPost]
        public async Task<ActionResult<EntrieViewModel>> Create(EntrieViewModel model)
        {
            var result = await this.entrieService.Create(model);

            if (result is null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
