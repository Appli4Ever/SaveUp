using Microsoft.EntityFrameworkCore;
using SaveUp.Models.ViewModels;
using SaveUp.Web.API.Entities;

namespace SaveUp.Web.API.Services;

public class EntrieService : IEntrieService
{
    private readonly SaveUpDbContext context;

    public EntrieService(SaveUpDbContext context)
    {
        this.context = context;
    }

    public async Task<List<EntrieViewModel>> GetAll()
    {
        return await this.context.Entries.Select(e => new EntrieViewModel()
        {
            Id = e.Id,
            Amount = e.Amount,
            Created = e.Created,
            Description = e.Description,
        }).ToListAsync();
    }

    public async Task<EntrieViewModel?> Delete(int id)
    {
        try
        {
            var entrie = await this.context.Entries.FindAsync(id);

            if (entrie is null)
            {
                return null;
            }

            var result = this.context.Entries.Remove(entrie);

            await this.context.SaveChangesAsync();

            return new EntrieViewModel()
            {
                Id = entrie.Id,
                Amount = entrie.Amount,
                Created = entrie.Created,
                Description = entrie.Description,
            };

        }
        catch (System.Exception e)
        {
            return null;
        }
    }

    public async Task<EntrieViewModel?> Create(EntrieViewModel model)
    {
        try
        {
            var entrie = new Entrie()
            {
                Amount = model.Amount,
                Created = model.Created,
                Description = model.Description,
            };

            await this.context.AddAsync(entrie);
            await this.context.SaveChangesAsync();

            model.Id = entrie.Id;
            return model;
        }
        catch
        {
            return null;
        }
    }
}