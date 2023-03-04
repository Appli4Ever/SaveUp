using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SaveUp.Models.ViewModels;
using SaveUp.Web.API.Migrations;
using SaveUp.Web.API.Services;
using SaveUp.Web.API.Tests.TestHelper;
using Entrie = SaveUp.Web.API.Entities.Entrie;

namespace SaveUp.Web.API.Tests.Services;

public class EntrieServiceTests
{
    [Test]
    public async Task GetAll_GibtAlleEintraegeZurueck()
    {
        var entries = TestDaten.GetEntries();
        var context = new SaveUpDbContextBuilder().AddEntries(entries).Build();

        var testee = new EntrieService(context);

        var result = await testee.GetAll();

        result.Count.Should().Be(3);
        result.Should().BeEquivalentTo(entries, e => e.ExcludingMissingMembers());
    }

    [Test]
    public async Task Delete_EintragVorhanden_LoeschtEintrag()
    {
        var entries = TestDaten.GetEntries();
        var context = new SaveUpDbContextBuilder().AddEntries(entries).Build();

        var testee = new EntrieService(context);

        var deleteEntrie = entries.First();
        await testee.Delete(deleteEntrie.Id);

        var result = await context.Entries.ToListAsync();

        result.Count.Should().Be(2);
        result.Should().NotContainEquivalentOf(deleteEntrie);
    }

    [Test]
    public async Task Delete_EintragVorhanden_GibtEintragZurueck()
    {
        var entries = TestDaten.GetEntries();
        var context = new SaveUpDbContextBuilder().AddEntries(entries).Build();

        var testee = new EntrieService(context);

        var deleteEntrie = entries.First();
        var result = await testee.Delete(deleteEntrie.Id);

        result.Should().BeEquivalentTo(deleteEntrie,
            e => e.Excluding(e => e.TenantId));
    }

    [Test]
    public async Task Delete_EintragNichtVorhanden_GibtNullZurueck()
    {
        var context = new SaveUpDbContextBuilder().Build();

        var testee = new EntrieService(context);

        var result = await testee.Delete(2);

        result.Should().BeNull();
    }

    [Test]
    public async Task Create_ErstelltNeuenEintrag()
    {
        var context = new SaveUpDbContextBuilder().Build();

        var testee = new EntrieService(context);

        var vm = new EntrieViewModel()
        {
            Description = "Kaugummi",
            Amount = 2.5,
            Created = DateTime.Now
        };

        await testee.Create(vm);

        var result = await context.Entries.FirstOrDefaultAsync();

        result.Should().BeEquivalentTo(vm);
    }

    [Test]
    public async Task Create_GibtErstelltenEintragZurueck()
    {
        var context = new SaveUpDbContextBuilder().Build();

        var testee = new EntrieService(context);

        var vm = new EntrieViewModel()
        {
            Description = "Kaugummi",
            Amount = 2.5,
            Created = DateTime.Now
        };

        var result = await testee.Create(vm);

        result.Should().BeEquivalentTo(vm);
    }

    [Test]
    public async Task DeleteRange_EintragVorhanden_GibtEintraegeZurueck()
    {
        var entries = TestDaten.GetEntries();
        var context = new SaveUpDbContextBuilder().AddEntries(entries).Build();

        var testee = new EntrieService(context);
        
        var result = await testee.DeleteRange(entries.Select(e => e.Id).ToList());

        result.Should().BeEquivalentTo(entries, e => e.ExcludingMissingMembers());
    }

    [Test]
    public async Task DeleteRange_EintragNichtVorhanden_GibtNullZurueck()
    {
        var entries = TestDaten.GetEntries();
        var context = new SaveUpDbContextBuilder().AddEntries(entries).Build();

        var testee = new EntrieService(context);

        var result = await testee.DeleteRange(new List<int>());

        result.Should().BeNull();
    }
}