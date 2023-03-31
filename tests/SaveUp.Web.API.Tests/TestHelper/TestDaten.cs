using SaveUp.Web.API.Entities;

namespace SaveUp.Web.API.Tests.TestHelper
{
    public static class TestDaten
    {
        public static List<Entrie> GetEntries()
        {
            return new List<Entrie>
            {
                new Entrie()
                {
                    Id = 1,
                    Amount = 2.2,
                    Created = DateTime.Today,
                    Description = "Kaugummi",
                    TenantId = 1
                },
                new Entrie()
                {
                    Id = 2,
                    Amount = 222,
                    Created = DateTime.Today,
                    Description = "YouTube",
                    TenantId = 1
                },
                new Entrie()
                {
                    Id = 3,
                    Amount = 13,
                    Created = DateTime.Today,
                    Description = "Auto",
                    TenantId = 1
                },
            };
        }
    }
}
