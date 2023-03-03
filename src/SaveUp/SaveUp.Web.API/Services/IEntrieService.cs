using SaveUp.Models.ViewModels;

namespace SaveUp.Web.API.Services;

public interface IEntrieService
{
    Task<List<EntrieViewModel>> GetAll();

    Task<EntrieViewModel?> Delete(int id);

    Task<EntrieViewModel?> Create(EntrieViewModel model);
}